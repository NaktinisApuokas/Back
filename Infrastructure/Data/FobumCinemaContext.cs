using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FobumCinema.Core.Entities;

namespace FobumCinema.Infrastructure.Data
{
    public class FobumCinemaContext : IdentityDbContext<User>
    {
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Screening> Screening { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentRating> CommentRating { get; set; }
        public DbSet<MovieMark> MovieMark { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<CinemaHall> CinemaHall { get; set; }
        public DbSet<HallType> HallType { get; set; }
        public DbSet<CinemaCompany> CinemaCompany { get; set; }
        public DbSet<SeatType> SeatType { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<SeatTypePrice> SeatTypePrice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=test");
        }
    }
}