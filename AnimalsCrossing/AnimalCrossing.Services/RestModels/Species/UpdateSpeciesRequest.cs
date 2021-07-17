using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Species
{
    public class UpdateSpeciesRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
