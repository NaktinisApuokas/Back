using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Data.Repositories
{
    public interface ICommentRatingRepository
    {
        Task<CommentRating> GetAsync(int commentRatingId);
        Task<List<CommentRating>> GetAllAsync(int movieId);
        Task InsertAsync(CommentRating commentRating);
        Task InsertRangeAsync(List<CommentRating> commentRatings);
        Task UpdateAsync(CommentRating commentRating);
    }

    public class CommentRatingRepository : ICommentRatingRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public CommentRatingRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<CommentRating> GetAsync(int commentRatingId)
        {
            return await _FobumCinemaContext.CommentRating.FirstOrDefaultAsync(o => o.Id == commentRatingId);
        }

        public async Task<List<CommentRating>> GetAllAsync(int commentId)
        {
            return await _FobumCinemaContext.CommentRating.Where(o => o.CommentId == commentId).ToListAsync();
        }

        public async Task InsertAsync(CommentRating commentRating)
        {
            _FobumCinemaContext.CommentRating.Add(commentRating);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<CommentRating> commentRatings)
        {
            _FobumCinemaContext.CommentRating.AddRange(commentRatings);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CommentRating commentRating)
        {
            _FobumCinemaContext.CommentRating.Update(commentRating);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
