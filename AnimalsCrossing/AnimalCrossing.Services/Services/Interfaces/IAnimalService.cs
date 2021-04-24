using AnimalCrossing.API.RestModels.Animals;
using AnimalCrossing.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IAnimalService
    {
        Task AddAsync(CreateAnimalRequest request);

        Task<List<Animal>> GetAllAsync();

        Task<Animal> GetById(int id);
    }
}
