using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{

    public class MovieRepository : IMovieRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public MovieRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Movie> GetAsync(int movieId)
        {
            return await _FobumCinemaContext.Movie.FirstOrDefaultAsync(o => o.Id == movieId);
        }

        public async Task<List<Movie>> GetAllAsync(int cinemaId)
        {
            return await _FobumCinemaContext.Movie.Where(o => o.CinemaId == cinemaId).ToListAsync();
        }
        public async Task<List<Movie>> GetAllAsync()
        {
            return await _FobumCinemaContext.Movie.ToListAsync();
        }
        public async Task<Movie> GetByCinemaIdAndTitleAsync(int cinemaId, string title)
        {
            return await _FobumCinemaContext.Movie.FirstOrDefaultAsync(o => o.CinemaId == cinemaId && o.Title == title);
        }

        public async Task<Movie> InsertAsync(Movie movie)
        {
            _FobumCinemaContext.Movie.Add(movie);
            await _FobumCinemaContext.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _FobumCinemaContext.Movie.Update(movie);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _FobumCinemaContext.Movie.Remove(movie);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
