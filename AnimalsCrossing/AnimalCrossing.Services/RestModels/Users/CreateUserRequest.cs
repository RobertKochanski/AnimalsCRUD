﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.Services.RestModels.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Subname { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }
    }
}
