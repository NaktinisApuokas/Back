using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
    public class MovieMarkRepository : IMovieMarkRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public MovieMarkRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<MovieMark> GetAsync(int movieMarkId)
        {
            return await _FobumCinemaContext.MovieMark.FirstOrDefaultAsync(o => o.Id == movieMarkId);
        }

        public async Task<MovieMark> GetByMovieAndNameAsync(int movieId, string name)
        {
            return await _FobumCinemaContext.MovieMark.FirstOrDefaultAsync(o => o.MovieId == movieId && o.Username == name);
        }

        public async Task<List<MovieMark>> GetAllAsync(string name)
        {
            return await _FobumCinemaContext.MovieMark.Where(o => o.Username == name).ToListAsync();
        }

        public async Task InsertAsync(MovieMark movieMark)
        {
            _FobumCinemaContext.MovieMark.Add(movieMark);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MovieMark movieMark)
        {
            _FobumCinemaContext.MovieMark.Remove(movieMark);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
