using AnimalCrossing.API.RestModels.Animals;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IAnimalService
    {
        Task Add(CreateAnimalRequest request);
    }
}
