using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Users;
using AnimalCrossing.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoggedInUserResponse> Authenticate(string username, string password);

        Task AddAsync(CreateUserRequest request);

        Task<List<UserViewModel>> GetAllAsync();

        Task<User> GetById(int id);
    }
}
