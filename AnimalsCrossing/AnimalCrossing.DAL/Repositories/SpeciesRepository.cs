using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Species> GetByIdAsync(int id)
        {
            return await _context.Species.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
