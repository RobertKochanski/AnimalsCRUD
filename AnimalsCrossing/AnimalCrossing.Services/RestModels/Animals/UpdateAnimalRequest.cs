using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Animals
{
    public class UpdateAnimalRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
