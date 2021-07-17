using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AnimalDBContext _context;

        public ReservationRepository(AnimalDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(a => a.Animal)
                .ThenInclude(b => b.Owner)
                .Include(a => a.Animal)
                .ThenInclude(c => c.Species)
                .ToListAsync();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _context.Reservations
                .Include(a => a.Animal)
                .ThenInclude(b => b.Owner)
                .Include(a => a.Animal)
                .ThenInclude(c => c.Species)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
