using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Animals;
using AnimalCrossing.Services.ViewModels.Animals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IAnimalService
    {
        Task AddAsync(CreateAnimalRequest request);

        Task<List<AnimalViewModel>> GetAllAsync();

        Task<Animal> GetById(int id);

        Task Remove(int id);

        Task EditAsync(UpdateAnimalRequest request);

        Task<List<AnimalPopulatedViewModel>> GetAllPopulatedAsync();
    }
}
