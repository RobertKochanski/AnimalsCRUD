using AnimalCrossing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossing.DAL.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        Task Add(Animal animal);
    }
}
