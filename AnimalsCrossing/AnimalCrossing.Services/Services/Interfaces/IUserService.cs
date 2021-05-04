using AnimalCrossing.Services.RestModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(CreateUserRequest request);
    }
}
