﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
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

        public async Task<CommentRating> GetByNameAndIdAsync(int commentId, string name)
        {
            return await _FobumCinemaContext.CommentRating.FirstOrDefaultAsync(o => o.CommentId == commentId && o.Username == name);
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

        public async Task UpdateAsync(CommentRating commentRating)
        {
            _FobumCinemaContext.CommentRating.Update(commentRating);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
