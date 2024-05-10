using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetAsync(int commentId);
        Task<List<Comment>> GetAllAsync(int movieId);
        Task InsertAsync(Comment comment);
        Task InsertRangeAsync(List<Comment> comments);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task DeleteAllAsync();
    }
}
