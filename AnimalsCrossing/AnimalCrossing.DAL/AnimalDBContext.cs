using AnimalCrossing.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.DAL
{
    public class AnimalDBContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public DbSet<Species> Species { get; set; }

        public DbSet<User> Users { get; set; }

        public AnimalDBContext(DbContextOptions<AnimalDBContext> options) : base(options)
        {

        }
    }
}
