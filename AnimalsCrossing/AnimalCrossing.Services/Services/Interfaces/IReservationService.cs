using AnimalCrossing.Services.RestModels.Reservations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddAsync(CreateReservationRequest request);
    }
}
