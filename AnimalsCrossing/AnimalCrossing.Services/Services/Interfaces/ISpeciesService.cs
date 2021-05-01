using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Species;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.Services.Services.Interfaces
{
    public interface ISpeciesService
    {
        Task AddAsync(CreateSpeciesRequest request);

        Task<Species> GetById(int id);
    }
}
