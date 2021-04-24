using AnimalCrossing.API.RestModels.Animals;
using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
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
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Imię nie może być puste.");
            }

            if(request.Age < 0)
            {
                throw new BadRequestException("Wiek zwierzęcia nie może być ujemny.");
            }
            
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

        public async Task<Animal> GetById(int id)
        {
            Animal result = await _animalRepository.GetById(id);

            if(result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }
    }
}
