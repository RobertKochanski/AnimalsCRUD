namespace AnimalCrossing.Services.RestModels.Animals
{
    public class CreateAnimalRequest
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int SpeciesId { get; set; }
    }
}
