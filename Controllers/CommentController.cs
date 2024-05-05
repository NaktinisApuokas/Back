using System.Data;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using FobumCinema.Auth.Model;
using FobumCinema.Data.Dtos.Comment;
using FobumCinema.Data.Entities;
using FobumCinema.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.Controllers
{
    [ApiController]
    [Route("api/cinemas/{CinemaId}/movies/{MovieId}/comment")]
    public class CommentController : ControllerBase
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _CommentRepository;
        private readonly ICommentRatingRepository _CommentRatingRepository;

        public CommentController(IMovieRepository MovieRepository, IMapper mapper, ICommentRepository CommentRepository, ICommentRatingRepository CommentRatingRepository)
        {
            _MovieRepository = MovieRepository;
            _mapper = mapper;
            _CommentRepository = CommentRepository;
            _CommentRatingRepository = CommentRatingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAllAsync(int movieId, string name)
        {
            var comments = await _CommentRepository.GetAllAsync(movieId);
            var commentDtos = new List<CommentDto>();

            foreach (var comment in comments)
            {
                var ratings = await _CommentRatingRepository.GetAllAsync(comment.Id);
                var totalScore = ratings.Sum(r => r.Score);

                var rating = await _CommentRatingRepository.GetByNameAndIdAsync(comment.Id, name);
                double score = rating?.Score ?? 0; 

                commentDtos.Add(new CommentDto(comment.Id, comment.Text, comment.Username, totalScore, score));
            }

            return commentDtos;
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetAsync(int commentId)
        {
            var movie = await _CommentRepository.GetAsync(commentId);
            if (movie == null) return NotFound();

            return Ok(_mapper.Map<CommentDto>(movie));
        }

        //insert
        [HttpPost]
        public async Task<ActionResult<CommentDto>> PostAsync(int cinemaId, int movieId, CreateCommentDto commentDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            Comment comment = _mapper.Map<Comment>(commentDto);
            comment.MovieId = movieId;

            await _CommentRepository.InsertAsync(comment);

            return Created($"/api/cinemas/{cinemaId}/movies/{movieId}/comments/{comment.Id}", _mapper.Map<CommentDto>(comment));
        }

        //update
        [HttpPut("{commentId}")]
        public async Task<ActionResult<CommentDto>> PutAsync(int movieId, int commentId, UpdateCommentDto commentDto)
        {
            var movie = await _MovieRepository.GetAllAsync(movieId);
            if (movie == null) return NotFound($"Couldn't find a movie with id of {movieId}");

            var oldComment = await _CommentRepository.GetAsync(commentId);
            if (oldComment == null)
                return NotFound();

            _mapper.Map(commentDto, oldComment);

            await _CommentRepository.UpdateAsync(oldComment);

            return Ok(_mapper.Map<CommentDto>(oldComment));
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteAsync(int commentId)
        {
            var comment = await _CommentRepository.GetAsync(commentId);
            if (comment == null)
                return NotFound();

            await _CommentRepository.DeleteAsync(comment);

            return NoContent();
        }
    }
}
