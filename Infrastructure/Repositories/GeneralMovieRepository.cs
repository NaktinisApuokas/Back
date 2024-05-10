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
