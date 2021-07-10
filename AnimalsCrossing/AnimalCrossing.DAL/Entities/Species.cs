using System.Collections.Generic;

namespace AnimalCrossing.DAL.Entities
{
    public class Species
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
