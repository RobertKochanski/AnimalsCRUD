using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task Add(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }
    }
}
