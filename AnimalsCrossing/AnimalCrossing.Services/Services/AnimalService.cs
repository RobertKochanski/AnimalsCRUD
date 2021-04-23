using AnimalCrossing.API.RestModels.Animals;
using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task AddAsync(CreateAnimalRequest request)
        {
            await _animalRepository.AddAsync(new Animal()
            {
                Name = request.Name,
                Age = request.Age
            });
        }

        public async Task<List<Animal>> GetAllAsync()
        {
            return await _animalRepository.GetAllAsync();
        }
    }
}
