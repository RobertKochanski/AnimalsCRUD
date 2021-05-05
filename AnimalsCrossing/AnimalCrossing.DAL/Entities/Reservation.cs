using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int userId { get; set; }

        public virtual User user { get; set; }

        public int animalId { get; set; }

        public virtual Animal animal { get; set; }

        public string comments { get; set; }

        public double cost { get; set; }
    }
}
