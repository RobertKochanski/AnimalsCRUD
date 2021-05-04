using AnimalCrossing.Services.ViewModels.Species;
using AnimalCrossing.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.ViewModels.Animals
{
    public class AnimalPopulatedViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public virtual SpeciesViewModel SpeciesViewModel { get; set; }

        public virtual UserViewModel UserViewModel { get; set; }
    }
}
