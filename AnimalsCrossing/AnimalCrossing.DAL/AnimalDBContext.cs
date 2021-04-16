using AnimalCrossing.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCrossing.DAL
{
    public class AnimalDBContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalDBContext(DbContextOptions<AnimalDBContext> options) : base(options)
        {

        }
    }
}
