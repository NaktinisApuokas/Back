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
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentRating> CommentRating { get; set; }
        public DbSet<MovieMark> MovieMark { get; set; }
        public DbSet<Review> Review { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=test");
        }
    }
}