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

        public async Task<Animal> GetById(int id)
        {
            return await _context.Animals.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
