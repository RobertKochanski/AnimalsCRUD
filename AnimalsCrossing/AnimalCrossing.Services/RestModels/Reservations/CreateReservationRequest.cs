using AnimalCrossing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Reservations
{
    public class CreateReservationRequest
    {
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int userId { get; set; }

        public int animalId { get; set; }

        public string comments { get; set; }

        public double cost { get; set; }
    }
}
