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
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IScreeningRepository _ScreeningRepository;
        private readonly IMapper _mapper;

        public GeneralMovieController(IGeneralMovieRepository MovieRepository , IMapper mapper, ICinemaRepository cinemaRepository, IScreeningRepository screeningRepository)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
            _CinemaRepository = cinemaRepository;
            _ScreeningRepository = screeningRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<GeneralMovieDto>> GetAllMoviesAsync()
        {
            var movies = await _MovieRepository.GetAllMoviesAsync();
            foreach(var movie in movies)
            {
                var cinema = await _CinemaRepository.Get(movie.CinemaId);
                movie.Cinema = cinema;
                var screenings = await _ScreeningRepository.GetAllAsync(movie.Id);
                movie.Screenings = screenings;
            }
            return movies.Select(o => _mapper.Map<GeneralMovieDto>(o));
        }
        
    }
}
   