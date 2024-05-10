using System.Data;
using System.Security.Claims;
using AutoMapper;
using FobumCinema.API.Models.Dtos.Review;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.API.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/movies/{MovieId}/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _ReviewRepository;

        public ReviewController(IMovieRepository MovieRepository, IMapper mapper, IReviewRepository ReviewRepository)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
            _ReviewRepository = ReviewRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewDto>> GetAllAsync(int movieId)
        {
            var movies = await _ReviewRepository.GetAllAsync(movieId);
            return movies.Select(o => _mapper.Map<ReviewDto>(o));
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> GetAsync(int reviewId)
        {
            var movie = await _ReviewRepository.GetAsync(reviewId);
            if (movie == null) return NotFound();

            return Ok(_mapper.Map<ReviewDto>(movie));
        }

        //insert
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostAsync(int cinemaId, int movieId, CreateReviewDto reviewDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            Review review = _mapper.Map<Review>(reviewDto);
            review.MovieId = movieId;

            await _ReviewRepository.InsertAsync(review);

            return Created($"/api/cinemas/{cinemaId}/movies/{movieId}/reviews/{review.Id}", _mapper.Map<ReviewDto>(review));
        }

        //update
        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> PutAsync(int movieId, int reviewId, UpdateReviewDto reviewDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var oldReview = await _ReviewRepository.GetAsync(reviewId);
            if (oldReview == null)
                return NotFound();

            _mapper.Map(reviewDto, oldReview);

            await _ReviewRepository.UpdateAsync(oldReview);

            return Ok(_mapper.Map<ReviewDto>(oldReview));
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteAsync(int reviewId)
        {
            var review = await _ReviewRepository.GetAsync(reviewId);
            if (review == null)
                return NotFound();

            await _ReviewRepository.DeleteAsync(review);

            return NoContent();
        }
    }
}
