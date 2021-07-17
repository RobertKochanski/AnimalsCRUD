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

        public async Task EditAsync(UpdateReservationRequest request)
        {
            Reservation reservationFromDb = await _reservationRepository.GetById(request.Id);

            if (reservationFromDb == null)
            {
                throw new BadRequestException("Nie istnieje rezerwacja o tym id.");
            }

            if (_animalRepository.GetById(request.AnimalId) == null)
            {
                throw new BadRequestException("Nie istnieje zwierzę o tym id.");
            }

            reservationFromDb.StartDate = request.StartDate;
            reservationFromDb.EndDate = request.EndDate;
            reservationFromDb.AnimalId = request.AnimalId;
            reservationFromDb.Comments = request.Comments;
            reservationFromDb.Cost = request.Cost;

            await _reservationRepository.SaveChangesAsync();
        }

        public async Task<List<ReservationViewModel>> GetAllAsync()
        {
            var reservationsFromDb = await _reservationRepository.GetAllAsync();

            if (reservationsFromDb == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<List<ReservationViewModel>>(reservationsFromDb);
        }

        public async Task<ReservationViewModel> GetById(int id)
        {
            var reservationFromDb = await _reservationRepository.GetById(id);

            if (reservationFromDb == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ReservationViewModel>(reservationFromDb);
        }

        public async Task Remove(int id)
        {
            Reservation reservation = await _reservationRepository.GetById(id);

            if (reservation == null)
                throw new BadRequestException("Rezerwacja o podanym Id nie istnieje.");

            _reservationRepository.Remove(reservation);
        }
    }
}
