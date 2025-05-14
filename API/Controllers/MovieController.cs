using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.Movie;
using FobumCinema.API.Models.Dtos.Ticket;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
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
        
        [HttpGet("upcoming")]
        public async Task<IEnumerable<ReturnUpcomingMovieDto>> GetAllUpcomingAsync()
        {
            var movies = await _MovieRepository.GetAllUpcomingAsync();

            return movies.Select(o => _mapper.Map<ReturnUpcomingMovieDto>(o));
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> GetAsync(int movieId)
        {
            var movie = await _MovieRepository.GetAsync(movieId);
            if (movie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<MovieDto>> PostAsync(int cinemaId, CreateMovieDto movieDto)
        {
            var cinema = await _CinemaRepository.Get(cinemaId);
            if (cinema == null) return NotFound($"Couldn't find a movie with id of {cinemaId}");

            var movie = _mapper.Map<Movie>(movieDto);
            movie.CinemaId = cinemaId;
            movie.IsUpcoming = 1;
            movie.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await _MovieRepository.InsertAsync(movie);

            return Created($"/api/cinemas/{cinemaId}/movies/{movie.Id}", _mapper.Map<MovieDto>(movie));
        }

        [HttpPost("Upcoming")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> PostUpcomingAsync(UpcomingMovieDto movieDto)
        {
            try
            {
                var movie = _mapper.Map<Movie>(movieDto);

                await _MovieRepository.InsertAsync(movie);

                return Ok(new { Status = "Movie successfully created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Failed to create Movie", Error = ex.Message });
            }
        }

        [HttpPut("{movieId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<MovieDto>> PutAsync(int movieId, UpdateMovieDto movieDto)
        {
            var oldMovie = await _MovieRepository.GetAsync(movieId);
            if (oldMovie == null)
                return NotFound();

            _mapper.Map(movieDto, oldMovie);

            await _MovieRepository.UpdateAsync(oldMovie);

            return Ok(_mapper.Map<MovieDto>(oldMovie));
        }

        [HttpPut("{movieId}/upcoming")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<MovieDto>> PutUpcomingAsync(int movieId, [FromBody] UpcomingMovieDto movieDto)
        {
            try
            {
                var oldMovie = await _MovieRepository.GetAsync(movieId);
                if (oldMovie == null)
                    return NotFound();

                _mapper.Map(movieDto, oldMovie);

                await _MovieRepository.UpdateAsync(oldMovie);

                return Ok(new { Status = "Movie successfully updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Failed to update Movie", Error = ex.Message });
            }
        }

        [HttpDelete("{movieId}")]
        [Authorize(Roles = UserRoles.Admin)]
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
