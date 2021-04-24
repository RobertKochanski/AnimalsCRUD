using AnimalCrossing.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        Task AddAsync(Animal animal);

        Task<List<Animal>> GetAllAsync();

        Task<Animal> GetById(int id);
    }
}
