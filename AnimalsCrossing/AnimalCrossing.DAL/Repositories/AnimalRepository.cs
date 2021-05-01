using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDBContext _context;

        public AnimalRepository(AnimalDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Animal>> GetAllAsync()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<List<Animal>> GetAllPopulatedAsync()
        {
            return await _context.Animals.Include(a => a.Species).ToListAsync();
        }

        public async Task<Animal> GetById(int id)
        {
            return await _context.Animals.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(Animal animal)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
