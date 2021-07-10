﻿using AnimalCrossing.Services.ViewModels.Animals;
using System;

namespace AnimalCrossing.Services.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public virtual AnimalPopulatedViewModel AnimalPopulatedViewModel { get; set; }

        public string comments { get; set; }

        public double cost { get; set; }
    }
}
