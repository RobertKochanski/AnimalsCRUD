using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Users;
using AnimalCrossing.Services.Services.Interfaces;
using AnimalCrossing.Services.ViewModels.User;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<LoggedInUserResponse> Authenticate(string username, string password)
        {
            var userFromDb = await _userRepository.GetByNameAsync(username);

            // return null if user not found
            if (userFromDb == null)
                throw new BadRequestException("Błędny login lub hasło.");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Role, userFromDb.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            LoggedInUserResponse loggedInUserResponse = new LoggedInUserResponse()
            {
                Id = userFromDb.Id,
                Username = userFromDb.Username,
                Role = userFromDb.Role,
                Token = tokenString
            };

            return loggedInUserResponse;
        }

        public async Task AddAsync(CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Subname))
            {
                throw new BadRequestException("Imię i nazwisko nie mogą być puste.");
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                throw new BadRequestException("Proszę podać hasło.");
            }

            await _userRepository.AddAsync(new User()
            {
                Name = request.Name,
                Subname = request.Subname,
                Username = request.Username,
                Password = request.Password,
                IsActive = true,
                Created = DateTime.Now,
                Role = Role.User
            });
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var usersFromDb = await _userRepository.GetAllAsync();

            return _mapper.Map<List<UserViewModel>>(usersFromDb);
        }

        public async Task<UserViewModel> GetById(int id, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (id != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");


            User userFromDb = await _userRepository.GetByIdAsync(id);

            if (userFromDb == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UserViewModel>(userFromDb);
        }
    }
}
