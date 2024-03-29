﻿using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.ViewModels.Animals;
using AnimalCrossing.Services.ViewModels.Reservation;
using AnimalCrossing.Services.ViewModels.Species;
using AnimalCrossing.Services.ViewModels.User;
using AutoMapper;

namespace AnimalCrossing.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalViewModel>();

            CreateMap<Animal, AnimalPopulatedViewModel>()
                .ForMember(a => a.SpeciesViewModel, opt => opt.MapFrom(s => s.Species))
                .ForMember(a => a.UserViewModel, opt => opt.MapFrom(s => s.Owner));

            CreateMap<Species, SpeciesViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(a => a.AnimalPopulatedViewModel, opt => opt.MapFrom(s => s.Animal));
        }
    }
}
