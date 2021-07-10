using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.ViewModels.User
{
    public class LoggedInUserResponse
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
    }
}
