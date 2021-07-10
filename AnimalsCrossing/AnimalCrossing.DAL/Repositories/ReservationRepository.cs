﻿using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
}
