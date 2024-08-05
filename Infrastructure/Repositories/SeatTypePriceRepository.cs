using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{

    public class SeatTypePriceRepository : ISeatTypePriceRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public SeatTypePriceRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<SeatTypePrice> GetAsync(int seatTypePriceId)
        {
            return await _FobumCinemaContext.SeatTypePrice.FirstOrDefaultAsync(o => o.Id == seatTypePriceId);
        }

        public async Task<List<SeatTypePrice>> GetAllAsync(int movieId)
        {
            return await _FobumCinemaContext.SeatTypePrice.Where(o => o.MovieId == movieId).ToListAsync();
        }

        public async Task<SeatTypePrice> InsertAsync(SeatTypePrice seatTypePrice)
        {
            _FobumCinemaContext.SeatTypePrice.Add(seatTypePrice);
            await _FobumCinemaContext.SaveChangesAsync();
            return seatTypePrice;
        }

        public async Task UpdateAsync(SeatTypePrice seatTypePrice)
        {
            _FobumCinemaContext.SeatTypePrice.Update(seatTypePrice);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SeatTypePrice seatTypePrice)
        {
            _FobumCinemaContext.SeatTypePrice.Remove(seatTypePrice);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
