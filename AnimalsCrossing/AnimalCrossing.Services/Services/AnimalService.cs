using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Animals;
using AnimalCrossing.Services.Services.Interfaces;
using AnimalCrossing.Services.ViewModels.Animals;
using AutoMapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AnimalService(IAnimalRepository animalRepository, ISpeciesRepository speciesRepository, IMapper mapper, IUserRepository userRepository)
        {
            _animalRepository = animalRepository;
            _speciesRepository = speciesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateAnimalRequest request, ClaimsPrincipal claimsPrincipal)
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

            if (await _userRepository.GetByIdAsync(int.Parse(claimsPrincipal.Identity.Name)) == null)
            {
                throw new BadRequestException("Dany właściciel nie istnieje");
            }

            await _animalRepository.AddAsync(new Animal()
            {
                Name = request.Name,
                Age = request.Age,
                SpeciesId = request.SpeciesId,
                OwnerId = int.Parse(claimsPrincipal.Identity.Name)
            });
        }

        public async Task EditAsync(UpdateAnimalRequest request, ClaimsPrincipal claimsPrincipal)
        {
            Animal animalFromDb = await _animalRepository.GetById(request.Id);

            if (animalFromDb == null)
            {
                throw new BadRequestException("Nie istnieje zwierzę o tym id.");
            }

            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (request.OwnerId != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");

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

            if (await _userRepository.GetByIdAsync(request.OwnerId) == null)
            {
                throw new BadRequestException("Dany właściciel nie istnieje");
            }

            animalFromDb.Name = request.Name;
            animalFromDb.Age = request.Age;
            animalFromDb.SpeciesId = request.SpeciesId;
            animalFromDb.OwnerId = request.OwnerId;

            await _animalRepository.SaveChangesAsync();
        }

        public async Task<List<AnimalViewModel>> GetAllAsync()
        {
            var animalsFromDb = await _animalRepository.GetAllAsync();

            return _mapper.Map<List<AnimalViewModel>>(animalsFromDb);
        }

        public async Task<List<AnimalPopulatedViewModel>> GetAllPopulatedAsync()
        {
            var animalsFromDb = await _animalRepository.GetAllPopulatedAsync();

            return _mapper.Map<List<AnimalPopulatedViewModel>>(animalsFromDb);
        }

        public async Task<Animal> GetById(int id, ClaimsPrincipal claimsPrincipal)
        {
            Animal animalFromDb = await _animalRepository.GetById(id);

            if(animalFromDb == null)
            {
                throw new NotFoundException();
            }

            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (animalFromDb.OwnerId != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");

            return animalFromDb;
        }

        public async Task<AnimalPopulatedViewModel> GetPopulatedByIdAsync(int id, ClaimsPrincipal claimsPrincipal)
        {
            var animalFromDb = await _animalRepository.GetPopulatedByIdAsync(id);

            if (animalFromDb == null)
            {
                throw new NotFoundException();
            }

            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (animalFromDb.OwnerId != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");

            return _mapper.Map<AnimalPopulatedViewModel>(animalFromDb);
        }

        public async Task Remove(int id, ClaimsPrincipal claimsPrincipal)
        {
            Animal animalFromDb = await _animalRepository.GetById(id);

            if(animalFromDb == null)
            {
                throw new BadRequestException("Nie istnieje zwierzę o tym id.");
            }

            var currentUserId = int.Parse(claimsPrincipal.Identity.Name);
            if (animalFromDb.OwnerId != currentUserId && !claimsPrincipal.IsInRole(Role.Admin))
                throw new BadRequestException("Brak dostępu.");

            _animalRepository.Remove(animalFromDb);
        }
    }
}
