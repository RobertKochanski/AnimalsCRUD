using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.ViewModels.Animals;
using AnimalCrossing.Services.ViewModels.Species;
using AnimalCrossing.Services.ViewModels.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCrossing.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalViewModel>();

            CreateMap<Animal, AnimalPopulatedViewModel>()
                .ForMember(a => a.SpeciesViewModel, opt => opt.MapFrom(s => s.Species));

            CreateMap<Species, SpeciesViewModel>();

            CreateMap<User, UserViewModel>();
        }
    }
}
