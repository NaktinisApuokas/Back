using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public CommentRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Comment> GetAsync(int commentId)
        {
            return await _FobumCinemaContext.Comment.FirstOrDefaultAsync(o => o.Id == commentId);
        }

        public async Task<List<Comment>> GetAllAsync(int movieId)
        {
            return await _FobumCinemaContext.Comment.Where(o => o.MovieId == movieId).ToListAsync();
        }

        public async Task InsertAsync(Comment comment)
        {
            _FobumCinemaContext.Comment.Add(comment);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<Comment> comments)
        {
            _FobumCinemaContext.Comment.AddRange(comments);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _FobumCinemaContext.Comment.Update(comment);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            _FobumCinemaContext.Comment.Remove(comment);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _FobumCinemaContext.Comment.RemoveRange(_FobumCinemaContext.Comment);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
