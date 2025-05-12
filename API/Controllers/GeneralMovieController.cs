using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FobumCinema.API.Models.Dtos.Movie;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/all_movies")]
    public class GeneralMovieController : ControllerBase
    {
        private readonly IGeneralMovieRepository _MovieRepository;
        private readonly ICinemaRepository _CinemaRepository;
        private readonly IScreeningRepository _ScreeningRepository;
        private readonly IMapper _mapper;

        public GeneralMovieController(IGeneralMovieRepository MovieRepository, IMapper mapper, ICinemaRepository cinemaRepository, IScreeningRepository screeningRepository)
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
            var cinemas = await _CinemaRepository.GetAll();

            var cinemaDict = cinemas.ToDictionary(c => c.Id, c => c.Name);

            return movies.Where(o => o.Date == "" || Convert.ToDateTime(o.Date)  < DateTime.Now.AddDays(1)).Select(movie => new GeneralMovieDto(
                CinemaName: cinemaDict.GetValueOrDefault(movie.CinemaId, "Unknown"),
                CinemaId: movie.CinemaId.ToString(),
                Id: movie.Id,
                Title: movie.Title,
                TitleEng: movie.TitleEng,
                Genre: movie.Genre.Replace("Žanras", ""),
                Duration: movie.Duration,
                Img: movie.Img,
                Description: movie.Description,
                TrailerURL: movie.TrailerURL
            )).OrderBy(o => o.Title);
        }

    }
}
