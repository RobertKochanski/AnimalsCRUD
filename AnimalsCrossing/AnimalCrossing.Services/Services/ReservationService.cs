using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Reservations;
using AnimalCrossing.Services.Services.Interfaces;
using AnimalCrossing.Services.ViewModels.Reservation;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IAnimalRepository animalRepository, IMapper mapper, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(CreateReservationRequest request)
        {
            if (await _animalRepository.GetById(request.AnimalId) == null)
            {
                throw new BadRequestException("Dane zwierzę nie istnieje");
            }

            await _reservationRepository.AddAsync(new Reservation()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                AnimalId = request.AnimalId,
                Comments = request.Comments,
                Cost = request.Cost
            });
        }

        public async Task<List<ReservationViewModel>> GetAllAsync()
        {
            var animalsFromDb = await _reservationRepository.GetAllAsync();

            return _mapper.Map<List<ReservationViewModel>>(animalsFromDb);
        }
    }
}
