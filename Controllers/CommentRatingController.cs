using System.Data;
using AutoMapper;
using FobumCinema.Data.Dtos.CommentRating;
using FobumCinema.Data.Entities;
using FobumCinema.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FobumCinema.Controllers
{
    [ApiController]
    [Route("api/commentRating")]
    public class CommentRatingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentRatingRepository _CommentRatingRepository;

        public CommentRatingController(IMapper mapper, ICommentRatingRepository CommentRatingRepository)
        {
            _mapper = mapper;
            _CommentRatingRepository = CommentRatingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentRatingDto>> GetAllAsync(int commentId)
        {
            var commentRatings = await _CommentRatingRepository.GetAllAsync(commentId);
            return commentRatings.Select(o => _mapper.Map<CommentRatingDto>(o));
        }

        [HttpGet("{commentRatingId}")]
        public async Task<ActionResult<CommentRatingDto>> GetAsync(int commentRatingId)
        {
            var commentRating = await _CommentRatingRepository.GetAsync(commentRatingId);
            if (commentRating == null) return NotFound();

            return Ok(_mapper.Map<CommentRatingDto>(commentRating));
        }

        //insert
        [HttpPost]
        public async Task<ActionResult<CommentRatingDto>> PostAsync(CreateCommentRatingDto commentRatingDto)
        {
            CommentRating commentRating = _mapper.Map<CommentRating>(commentRatingDto);

            await _CommentRatingRepository.InsertAsync(commentRating);

            return Created($"/api/commentRatings/{commentRating.Id}", _mapper.Map<CommentRatingDto>(commentRating));
        }

        //update
        [HttpPut("{commentRatingId}")]
        public async Task<ActionResult<CommentRatingDto>> PutAsync(int commentRatingId, UpdateCommentRatingDto commentRatingDto)
        {
            var oldCommentRating = await _CommentRatingRepository.GetAsync(commentRatingId);
            if (oldCommentRating == null)
                return NotFound();

            _mapper.Map(commentRatingDto, oldCommentRating);

            await _CommentRatingRepository.UpdateAsync(oldCommentRating);

            return Ok(_mapper.Map<CommentRatingDto>(oldCommentRating));
        }
    }
}
