using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public string Comments { get; set; }

        public double Cost { get; set; }
    }
}
