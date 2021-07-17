using AnimalCrossing.Services.ViewModels.Animals;
using System;

namespace AnimalCrossing.Services.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual AnimalPopulatedViewModel AnimalPopulatedViewModel { get; set; }

        public string Comments { get; set; }

        public double Cost { get; set; }
    }
}
