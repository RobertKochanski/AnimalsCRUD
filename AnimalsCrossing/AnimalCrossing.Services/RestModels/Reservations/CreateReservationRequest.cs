using AnimalCrossing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Reservations
{
    public class CreateReservationRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AnimalId { get; set; }

        public string Comments { get; set; }

        public double Cost { get; set; }
    }
}
