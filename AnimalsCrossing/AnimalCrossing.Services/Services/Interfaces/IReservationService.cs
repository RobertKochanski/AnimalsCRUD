using AnimalCrossing.Services.RestModels.Reservations;
using AnimalCrossing.Services.ViewModels.Reservation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddAsync(CreateReservationRequest request);

        Task<List<ReservationViewModel>> GetAllAsync();

        Task<ReservationViewModel> GetById(int id, ClaimsPrincipal claimsPrincipal);

        Task Remove(int id, ClaimsPrincipal claimsPrincipal);

        Task EditAsync(UpdateReservationRequest request, ClaimsPrincipal claimsPrincipal);
    }
}
