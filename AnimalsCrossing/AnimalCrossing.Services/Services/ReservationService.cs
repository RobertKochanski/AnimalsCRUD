using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using AnimalCrossing.Services.Exceptions;
using AnimalCrossing.Services.RestModels.Reservations;
using AnimalCrossing.Services.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (await _animalRepository.GetById(request.animalId) == null)
            {
                throw new BadRequestException("Dane zwierzę nie istnieje");
            }

            if (await _userRepository.GetByIdAsync(request.userId) == null)
            {
                throw new BadRequestException("Dany właściciel nie istnieje");
            }

            await _reservationRepository.AddAsync(new Reservation()
            {
                startDate = request.startDate,
                endDate = request.endDate,
                userId = request.userId,
                animalId = request.animalId,
                comments = request.comments,
                cost = request.cost
            });
        }
    }
}
