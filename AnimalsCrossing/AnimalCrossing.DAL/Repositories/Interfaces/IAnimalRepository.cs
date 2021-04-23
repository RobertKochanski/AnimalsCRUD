using AnimalCrossing.DAL.Entities;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        Task Add(Animal animal);
    }
}
