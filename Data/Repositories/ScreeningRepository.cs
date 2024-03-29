﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Data.Repositories
{
    public interface IScreeningRepository
    {
        Task<Screening> GetAsync(int screeningId);
        Task<List<Screening>> GetAllAsync(int movieId);
        Task InsertAsync(Screening screening);
        Task InsertRangeAsync(List<Screening> screenings);
        Task UpdateAsync(Screening screening);
        Task DeleteAsync(Screening screening);
        Task DeleteAllAsync();
    }

    public class ScreeningRepository : IScreeningRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public ScreeningRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Screening> GetAsync(int screeningId)
        {
            return await _FobumCinemaContext.Screening.FirstOrDefaultAsync(o => o.Id == screeningId);
        }

        public async Task<List<Screening>> GetAllAsync(int movieId)
        {
            return await _FobumCinemaContext.Screening.Where(o => o.MovieId == movieId).ToListAsync();
        }

        public async Task InsertAsync(Screening screening)
        {
            _FobumCinemaContext.Screening.Add(screening);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<Screening> screenings)
        {
            _FobumCinemaContext.Screening.AddRange(screenings);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Screening screening)
        {
            _FobumCinemaContext.Screening.Update(screening);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Screening screening)
        {
            _FobumCinemaContext.Screening.Remove(screening);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _FobumCinemaContext.Screening.RemoveRange(_FobumCinemaContext.Screening);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
