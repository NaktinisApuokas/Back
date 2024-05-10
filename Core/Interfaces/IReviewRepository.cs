using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> GetAsync(int reviewId);
        Task<List<Review>> GetAllAsync(int movieId);
        Task InsertAsync(Review review);
        Task InsertRangeAsync(List<Review> reviews);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);
        Task DeleteAllAsync();
    }

}
