using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public ReviewRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Review> GetAsync(int reviewId)
        {
            return await _FobumCinemaContext.Review.FirstOrDefaultAsync(o => o.Id == reviewId);
        }

        public async Task<List<Review>> GetAllAsync(int movieId)
        {
            return await _FobumCinemaContext.Review.Where(o => o.MovieId == movieId).ToListAsync();
        }

        public async Task InsertAsync(Review review)
        {
            _FobumCinemaContext.Review.Add(review);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<Review> reviews)
        {
            _FobumCinemaContext.Review.AddRange(reviews);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _FobumCinemaContext.Review.Update(review);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _FobumCinemaContext.Review.Remove(review);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _FobumCinemaContext.Review.RemoveRange(_FobumCinemaContext.Review);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
