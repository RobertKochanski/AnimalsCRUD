using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly AnimalDBContext _context;

        public SpeciesRepository(AnimalDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Species species)
        {
            await _context.Species.AddAsync(species);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Species>> GetAllAsync()
        {
            return await _context.Species.ToListAsync();
        }

        public async Task<Species> GetByIdAsync(int id)
        {
            return await _context.Species.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(Species species)
        {
            _context.Species.Remove(species);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
