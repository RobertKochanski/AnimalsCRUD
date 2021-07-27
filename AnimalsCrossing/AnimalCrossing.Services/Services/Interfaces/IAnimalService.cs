using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Animals;
using AnimalCrossing.Services.ViewModels.Animals;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface IAnimalService
    {
        Task AddAsync(CreateAnimalRequest request, ClaimsPrincipal claimsPrincipal);

        Task<List<AnimalViewModel>> GetAllAsync();

        Task<Animal> GetById(int id, ClaimsPrincipal claimsPrincipal);

        Task<List<AnimalPopulatedViewModel>> GetAllPopulatedAsync();

        Task<AnimalPopulatedViewModel> GetPopulatedByIdAsync(int id, ClaimsPrincipal claimsPrincipal);

        Task Remove(int id, ClaimsPrincipal claimsPrincipal);

        Task EditAsync(UpdateAnimalRequest request, ClaimsPrincipal claimsPrincipal);
    }
}
