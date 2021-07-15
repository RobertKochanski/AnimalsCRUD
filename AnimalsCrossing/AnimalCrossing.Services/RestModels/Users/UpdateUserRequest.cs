using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
