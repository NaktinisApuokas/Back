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


        [HttpGet]
        public async Task<ActionResult<CommentRatingDto>> GetByNameAndIdAsync(int commentId, string name)
        {
            var commentRating = await _CommentRatingRepository.GetByNameAndIdAsync(commentId, name);
            if (commentRating == null) return NotFound();

            return Ok(_mapper.Map<CommentRatingDto>(commentRating));
        }


        [HttpGet("{commentRatingId}")]
        public async Task<ActionResult<CommentRatingDto>> GetAsync(int commentRatingId)
        {
            var commentRating = await _CommentRatingRepository.GetAsync(commentRatingId);
            if (commentRating == null) return NotFound();

            return Ok(_mapper.Map<CommentRatingDto>(commentRating));
        }


        [HttpPut]
        public async Task<ActionResult<CommentRatingDto>> ManageAsync(ManageCommentRatingDto commentRatingDto)
        {

            var oldCommentRating = await _CommentRatingRepository.GetByNameAndIdAsync(commentRatingDto.CommentId, commentRatingDto.Username);
            if (oldCommentRating == null)
            {
                CommentRating createCommentRating = _mapper.Map<CommentRating>(commentRatingDto);

                await _CommentRatingRepository.InsertAsync(createCommentRating);

                return Created($"/api/commentRatings/{createCommentRating.Id}", _mapper.Map<CommentRatingDto>(createCommentRating));
            }

            _mapper.Map(commentRatingDto, oldCommentRating);

            await _CommentRatingRepository.UpdateAsync(oldCommentRating);

            return Ok(_mapper.Map<CommentRatingDto>(oldCommentRating));
        }
    }
}
