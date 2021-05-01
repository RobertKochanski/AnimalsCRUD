using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Animals;
using AnimalCrossing.Services.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ISpeciesRepository _speciesRepository;

        public AnimalService(IAnimalRepository animalRepository, ISpeciesRepository speciesRepository)
        {
            _animalRepository = animalRepository;
            _speciesRepository = speciesRepository;
        }

        public async Task AddAsync(CreateAnimalRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Imię nie może być puste.");
            }

            if (request.Age < 0)
            {
                throw new BadRequestException("Wiek zwierzęcia nie może być ujemny.");
            }

            if (await _speciesRepository.GetByIdAsync(request.SpeciesId) == null)
            {
                throw new BadRequestException("Dany gatunek nie istnieje");
            }
            
            await _animalRepository.AddAsync(new Animal()
            {
                Name = request.Name,
                Age = request.Age,
                SpeciesId = request.SpeciesId
            });
        }

        public async Task EditAsync(UpdateAnimalRequest request)
        {
            Animal animalFromDb = await _animalRepository.GetById(request.Id);

            if (animalFromDb == null)
            {
                throw new BadRequestException("Nie istnieje zwierzę o tym id.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Imię nie może być puste.");
            }

            if (request.Age < 0)
            {
                throw new BadRequestException("Wiek zwierzęcia nie może być ujemny.");
            }

            if (await _speciesRepository.GetByIdAsync(request.SpeciesId) == null)
            {
                throw new BadRequestException("Dany gatunek nie istnieje");
            }

            animalFromDb.Name = request.Name;
            animalFromDb.Age = request.Age;
            animalFromDb.SpeciesId = request.SpeciesId;

            await _animalRepository.SaveChangesAsync();
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

        public async Task Remove(int id)
        {
            Animal result = await _animalRepository.GetById(id);

            if(result == null)
            {
                throw new BadRequestException("Nie istnieje zwierzę o tym id.");
            }

            _animalRepository.Remove(result);
        }
    }
}
