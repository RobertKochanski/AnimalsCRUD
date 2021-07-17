using AnimalCrossing.Services.RestModels.Species;
using AnimalCrossing.Services.ViewModels.Species;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface ISpeciesService
    {
        Task AddAsync(CreateSpeciesRequest request);

        Task<List<SpeciesViewModel>> GetAllsync();

        Task<SpeciesViewModel> GetByIdAsync(int id);

        Task EditAsync(UpdateSpeciesRequest request);

        Task Remove(int id);
    }
}
