using AnimalCrossing.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);

        Task<List<Reservation>> GetAllAsync();

        Task<Reservation> GetById(int id);

        void Remove(Reservation reservation);

        Task SaveChangesAsync();
    }
}
