using AnimalCrossing.Services.RestModels.Reservations;
using AnimalCrossing.Services.ViewModels.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddAsync(CreateReservationRequest request);

        Task<List<ReservationViewModel>> GetAllAsync();
    }
}
