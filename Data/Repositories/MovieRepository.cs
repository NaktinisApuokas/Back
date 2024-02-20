using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Data.Dtos.Movie;
using FobumCinema.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Data.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> GetAsync(int movieId);

        Task<List<Movie>> GetAllAsync(int cinemaId);

        Task<Movie> GetByCinemaIdAndTitleAsync(int cinemaId, string title);

        Task InsertAsync(Movie movie);

        Task UpdateAsync(Movie movie);

        Task DeleteAsync(Movie movie);
    }

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
        public async Task<Movie> GetByCinemaIdAndTitleAsync(int cinemaId, string title)
        {
            return await _FobumCinemaContext.Movie.FirstOrDefaultAsync(o => o.CinemaId == cinemaId && o.Title == title);
        }

        public async Task InsertAsync(Movie movie)
        {
            _FobumCinemaContext.Movie.Add(movie);
            await _FobumCinemaContext.SaveChangesAsync();
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
