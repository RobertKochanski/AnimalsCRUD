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

            if (!VerifyPasswordHash(password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
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

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            await _userRepository.AddAsync(new User()
            {
                Name = request.Name,
                Subname = request.Subname,
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
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

        public async Task EditAsync(UpdateUserRequest request, ClaimsPrincipal claimsPrincipal)
        {
            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (request.Id != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");

            User userFromDb = await _userRepository.GetByIdAsync(request.Id);

            if (userFromDb == null)
            {
                throw new BadRequestException("Nie istnieje użytkownik o tym id.");
            }

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Subname)
                || string.IsNullOrEmpty(request.Username))
            {
                throw new BadRequestException("Nie wprowadzono pełnych danych");
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                userFromDb.PasswordHash = passwordHash;
                userFromDb.PasswordSalt = passwordSalt;
            }
            
            userFromDb.Name = request.Name;
            userFromDb.Subname = request.Subname;
            userFromDb.Username = request.Username;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserViewModel> GetMyInfo(ClaimsPrincipal user)
        {
            var currentUserId = int.Parse(user.Identity.Name);

            User userFromDb = await _userRepository.GetByIdAsync(currentUserId);

            if (userFromDb == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UserViewModel>(userFromDb);
        }

        public async Task Remove(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new BadRequestException("Gatunek o tym Id nie istnieje.");

            _userRepository.Remove(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
