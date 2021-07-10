using System;

namespace AnimalCrossing.Services.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subname { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }
    }
}
