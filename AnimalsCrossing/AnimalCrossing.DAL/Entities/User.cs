using System;
using System.Collections.Generic;

namespace AnimalCrossing.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subname { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public string Role { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
