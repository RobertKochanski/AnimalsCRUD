using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Users;
using AnimalCrossing.Services.Services.Interfaces;
using AnimalCrossing.Services.ViewModels.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
                Password = request.Password,
                IsActive = true,
                Created = DateTime.Now
            });
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var animalsFromDb = await _userRepository.GetAllAsync();

            return _mapper.Map<List<UserViewModel>>(animalsFromDb);
        }
    }
}
