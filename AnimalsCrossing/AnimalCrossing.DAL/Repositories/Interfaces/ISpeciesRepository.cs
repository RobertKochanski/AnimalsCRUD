using AnimalCrossing.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface ISpeciesRepository
    {
        Task AddAsync(Species species);

        Task<Species> GetByIdAsync(int id);

        Task<List<Species>> GetAllAsync();

        Task SaveChangesAsync();

        void Remove(Species species);
    }
}
