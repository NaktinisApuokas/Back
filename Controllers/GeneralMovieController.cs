using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.Data.Dtos.Movie;
using FobumCinema.Data.Entities;
using FobumCinema.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.Controllers
{
    [ApiController]
    [Route("api/all_movies")]
    public class GeneralMovieController : ControllerBase
    {
        private readonly IGeneralMovieRepository _MovieRepository;
        private readonly IMapper _mapper;

        public GeneralMovieController(IGeneralMovieRepository MovieRepository, IMapper mapper)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _MovieRepository.GetAllMoviesAsync();
            return movies.Select(o => _mapper.Map<MovieDto>(o));
        }
        
    }
}
   