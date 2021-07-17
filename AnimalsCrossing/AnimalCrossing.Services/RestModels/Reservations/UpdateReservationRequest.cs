using System;

namespace AnimalCrossing.Services.RestModels.Reservations
{
    public class UpdateReservationRequest
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AnimalId { get; set; }

        public string Comments { get; set; }

        public double Cost { get; set; }
    }
}
