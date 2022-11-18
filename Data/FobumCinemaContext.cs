using FobumCinema.Data.Dtos.Auth;
using FobumCinema.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FobumCinema.Data
{
    public class FobumCinemaContext : IdentityDbContext<FobumCinemaUser>
    {
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Screening> Screening { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! DON'T STORE THE REAL CONNECTION STRING THE IN PUBLIC REPO !!!
            // Use secret managers provided by your chosen cloud provider
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=RestDemo4");
        }
    }
}