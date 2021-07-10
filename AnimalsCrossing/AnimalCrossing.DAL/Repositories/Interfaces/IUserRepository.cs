using AnimalCrossing.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> GetByNameAsync(string username);
    }
}
