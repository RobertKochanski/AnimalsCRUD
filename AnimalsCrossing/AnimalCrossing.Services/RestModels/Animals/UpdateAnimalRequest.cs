﻿namespace AnimalCrossing.Services.RestModels.Animals
{
    public class UpdateAnimalRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int SpeciesId { get; set; }

        public int OwnerId { get; set; }
    }
}
