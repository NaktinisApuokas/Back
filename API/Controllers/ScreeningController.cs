using System.Data;
using AutoMapper;
using FobumCinema.API.Auth.Model;
using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/movies/{MovieId}/screening")]
    public class ScreeningController : ControllerBase
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IMapper _mapper;
        private readonly IScreeningRepository _ScreeningRepository;

        public ScreeningController(IMovieRepository MovieRepository,
            ICinemaRepository CinemaRepository,
            IMapper mapper,
            IScreeningRepository ScreeningRepository)
        {
            _MovieRepository = MovieRepository;
            _CinemaRepository = CinemaRepository;
            _mapper = mapper;
            _ScreeningRepository = ScreeningRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ScreeningDto>> GetAllAsync(int movieId)
        {
            var movies = await _ScreeningRepository.GetAllAsync(movieId);
            return movies.Select(o => _mapper.Map<ScreeningDto>(o));
        }

        [HttpGet("{screeningId}")]
        public async Task<ActionResult<ScreeningInfoDto>> GetAsync(int screeningId)
        {
            var screening = await _ScreeningRepository.GetAsync(screeningId);
            if (screening == null) return NotFound();

            var movie = await _MovieRepository.GetAsync(screening.MovieId);
            if (movie == null) return NotFound();

            var cinema = await _CinemaRepository.Get(movie.CinemaId);
            if (cinema == null) return NotFound();

            var dto = new ScreeningInfoDto(
                ScreeningId: screening.Id,
                Time: screening.Time,
                MovieTitle: movie.Title,
                MovieTitleEng: movie.TitleEng, 
                CinemaName: cinema.Name,
                ScreeningDateTime: screening.Date + " " + screening.Time 
            );

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ScreeningDto>> PostAsync(int cinemaId, int movieId, CreateScreeningDto screeningDto)
        {
            var movie = await _MovieRepository.GetAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var screening = _mapper.Map<Screening>(screeningDto);
            screening.MovieId = movieId;

            await _ScreeningRepository.InsertAsync(screening);

            return Created($"/api/cinemas/{cinemaId}/movies/{movieId}/screenings/{screening.Id}", _mapper.Map<ScreeningDto>(screening));
        }

        //update
        [HttpPut("{screeningId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ScreeningDto>> PutAsync(int movieId, int screeningId, UpdateScreeningDto screeningDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var oldScreening = await _ScreeningRepository.GetAsync(screeningId);
            if (oldScreening == null)
                return NotFound();

            _mapper.Map(screeningDto, oldScreening);

            await _ScreeningRepository.UpdateAsync(oldScreening);

            return Ok(_mapper.Map<ScreeningDto>(oldScreening));
        }

        [HttpDelete("{screeningId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int screeningId)
        {
            var screening = await _ScreeningRepository.GetAsync(screeningId);
            if (screening == null)
                return NotFound();

            await _ScreeningRepository.DeleteAsync(screening);

            return NoContent();
        }
    }
}
