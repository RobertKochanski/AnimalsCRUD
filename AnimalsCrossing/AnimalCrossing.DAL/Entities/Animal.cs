namespace AnimalCrossing.DAL.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}
