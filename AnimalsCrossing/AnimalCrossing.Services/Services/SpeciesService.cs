using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Species;
using AnimalCrossing.Services.Services.Interfaces;
using AnimalCrossing.Services.ViewModels.Species;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public SpeciesService(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateSpeciesRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException("Imię nie może być puste.");
            }

            await _speciesRepository.AddAsync(new Species()
            {
                Name = request.Name
            });
        }

        public async Task EditAsync(UpdateSpeciesRequest request)
        {
            var speciesFromDb = await _speciesRepository.GetByIdAsync(request.Id);

            if (request.Name == null)
            {
                throw new BadRequestException("Nie podano nazwy");
            }

            speciesFromDb.Name = request.Name;

            await _speciesRepository.SaveChangesAsync();
        }

        public async Task<List<SpeciesViewModel>> GetAllsync()
        {
            var speciesFromDb = await _speciesRepository.GetAllAsync();

            return _mapper.Map<List<SpeciesViewModel>>(speciesFromDb);
        }

        public async Task<SpeciesViewModel> GetByIdAsync(int id)
        {
            Species speciesFromDb = await _speciesRepository.GetByIdAsync(id);

            if (speciesFromDb == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<SpeciesViewModel>(speciesFromDb);
        }

        public async Task Remove(int id)
        {
            Species species = await _speciesRepository.GetByIdAsync(id);

            if (species == null)
                throw new BadRequestException("Gatunek o tym Id nie istnieje.");

            _speciesRepository.Remove(species);
        }
    }
}
