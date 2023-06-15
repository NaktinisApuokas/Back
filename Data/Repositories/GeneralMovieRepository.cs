using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Data.Dtos.Movie;
using FobumCinema.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Data.Repositories
{
    public interface IGeneralMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
    }

    public class GeneralMovieRepository : IGeneralMovieRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public GeneralMovieRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _FobumCinemaContext.Movie.ToListAsync();
        }
    }
}
