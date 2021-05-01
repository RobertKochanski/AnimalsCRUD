using AnimalCrossing.Services.ViewModels.Species;
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

        public virtual SpeciesViewModel Species { get; set; }
    }
}
