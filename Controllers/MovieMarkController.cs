﻿using System.Data;
using AutoMapper;
using FobumCinema.Auth.Model;
using FobumCinema.Data.Dtos.Movie;
using FobumCinema.Data.Dtos.MovieMark;
using FobumCinema.Data.Dtos.Screening;
using FobumCinema.Data.Entities;
using FobumCinema.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/movies/{MovieId}/screening")]
    public class MovieMarkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieMarkRepository _MovieMarkRepository;
        private readonly IMovieRepository _MovieRepository;

        public MovieMarkController(IMapper mapper, IMovieMarkRepository MovieMarkRepository, IMovieRepository movieRepository)
        {
            _MovieRepository = movieRepository;
            _mapper = mapper;
            _MovieMarkRepository = MovieMarkRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieMarkDto>> GetAllAsync(string username)
        {
            var movieMarks = await _MovieMarkRepository.GetAllAsync(username);
            return movieMarks.Select(o => _mapper.Map<MovieMarkDto>(o));
        }

        [HttpGet("{movieMarkId}")]
        public async Task<ActionResult<MovieMarkDto>> GetAsync(int movieMarkId)
        {
            var movieMark = await _MovieMarkRepository.GetAsync(movieMarkId);
            if (movieMark == null) return NotFound();

            return Ok(_mapper.Map<MovieMarkDto>(movieMark));
        }

        //insert
        [HttpPost]
        [Authorize(Roles = UserRoles.SimpleUser)]
        public async Task<ActionResult<MovieMarkDto>> PostAsync(int cinemaId, int movieId, CreateMovieMarkDto movieMarkDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var movieMark = _mapper.Map<MovieMark>(movieMarkDto);
            movieMark.MovieId = movieId;

            await _MovieMarkRepository.InsertAsync(movieMark);

            return Created($"/api/cinemas/{cinemaId}/movies/{movieId}/screenings/{movieMark.Id}", _mapper.Map<MovieMarkDto>(movieMark));
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteAsync(int movieId, string username)
        {
            var movie = await _MovieMarkRepository.GetByMovieAndNameAsync(movieId, username);
            if (movie == null)
                return NotFound();

            await _MovieMarkRepository.DeleteAsync(movie);

            return NoContent();
        }
    }
}
