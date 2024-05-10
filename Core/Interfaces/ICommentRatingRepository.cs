using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ICommentRatingRepository
    {

        Task<CommentRating> GetByNameAndIdAsync(int commentId, string name);
        Task<CommentRating> GetAsync(int commentRatingId);
        Task<List<CommentRating>> GetAllAsync(int movieId);
        Task InsertAsync(CommentRating commentRating);
        Task UpdateAsync(CommentRating commentRating);
    }
}
