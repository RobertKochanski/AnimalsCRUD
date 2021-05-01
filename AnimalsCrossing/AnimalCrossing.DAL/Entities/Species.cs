using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.DAL.Entities
{
    public class Species
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
