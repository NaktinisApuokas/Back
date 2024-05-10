using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
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
