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
    [Route("api/cinemas/{CinemaId}/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IScreeningRepository _ScreeningRepository;
        private readonly IMovieMarkRepository _MovieMarkRepository;
        private readonly IMapper _mapper;
        

        public MovieController(IMovieRepository MovieRepository,
            IScreeningRepository ScreeningRepository, IMapper mapper,
            ICinemaRepository CinemaRepository,
            IMovieMarkRepository MovieMarkRepository)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
            _CinemaRepository = CinemaRepository;
            _ScreeningRepository = ScreeningRepository;
            _MovieMarkRepository = MovieMarkRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAllAsync(int cinemaId)
        {
            var movies = await _MovieRepository.GetAllAsync(cinemaId);
            foreach (var movie in movies)
            {
                var screenings = await _ScreeningRepository.GetAllAsync(movie.Id);
                movie.Screenings = screenings;
            }
            return movies.Select(o => _mapper.Map<MovieDto>(o));
        }

        [HttpGet("favorite")]
        public async Task<IEnumerable<MovieDto>> GetAllFavoriteAsync(string name)
        {
            var movies = await _MovieRepository.GetAllAsync();
            var moviesWithMark = new List<Movie>();

            foreach (var movie in movies)
            {
                var movieMark = await _MovieMarkRepository.GetByMovieAndNameAsync(movie.Id, name);

                if (movieMark != null)
                {
                    movie.IsMarked = true;

                    var screenings = await _ScreeningRepository.GetAllAsync(movie.Id);
                    movie.Screenings = screenings;

                    moviesWithMark.Add(movie);
                }
            }

            return moviesWithMark.Select(o => _mapper.Map<MovieDto>(o));
        }

        [HttpGet("detailed")]
        public async Task<IEnumerable<MovieDto>> GetAllDetailedAsync(int cinemaId, string name)
        {
            var movies = await _MovieRepository.GetAllAsync(cinemaId);
            var moviesWithScreenings = new List<Movie>();

            foreach (var movie in movies)
            {
                var screenings = await _ScreeningRepository.GetAllAsync(movie.Id);

                var movieMark = await _MovieMarkRepository.GetByMovieAndNameAsync(movie.Id, name);

                movie.IsMarked = false;

                if (movieMark != null)
                {
                    movie.IsMarked = true;
                }


                if (screenings.Any())
                {
                    movie.Screenings = screenings;
                    moviesWithScreenings.Add(movie);
                }
            }

            return moviesWithScreenings.Select(o => _mapper.Map<MovieDto>(o));
        }


        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> GetAsync(int movieId)
        {
            var movie = await _MovieRepository.GetAsync(movieId);
            if (movie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostAsync(int cinemaId, CreateMovieDto movieDto)
        {
            var cinema = await _CinemaRepository.Get(cinemaId);
            if (cinema == null) return NotFound($"Couldn't find a movie with id of {cinemaId}");

            var movie = _mapper.Map<Movie>(movieDto);
            movie.CinemaId = cinemaId; 

            await _MovieRepository.InsertAsync(movie);

            return Created($"/api/cinemas/{cinemaId}/movies/{movie.Id}", _mapper.Map<MovieDto>(movie));
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult<MovieDto>> PutAsync(int CinemaId, int movieId, UpdateMovieDto movieDto)
        {
            var cinema = await _CinemaRepository.Get(CinemaId);
            if (cinema == null) return NotFound($"Couldn't find a movie with id of {CinemaId}");

            var oldMovie = await _MovieRepository.GetAsync(movieId);
            if (oldMovie == null)
                return NotFound();

            _mapper.Map(movieDto, oldMovie);

            await _MovieRepository.UpdateAsync(oldMovie);

            return Ok(_mapper.Map<MovieDto>(oldMovie));
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteAsync(int movieId)
        {
            var movie = await _MovieRepository.GetAsync(movieId);
            if (movie == null)
                return NotFound();

            await _MovieRepository.DeleteAsync(movie);

            return Ok("Movie deleted successfully");
        }
    }
}
   