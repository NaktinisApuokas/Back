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

    public class SeatRepository : ISeatRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public SeatRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<Seat> GetAsync(int seatId)
        {
            return await _FobumCinemaContext.Seat.FirstOrDefaultAsync(o => o.Id == seatId);
        }

        public async Task<List<Seat>> GetAllAsync(int cinemaHallId)
        {
            return await _FobumCinemaContext.Seat.Where(o => o.CinemaHallId == cinemaHallId).ToListAsync();
        }

        public async Task<Seat> InsertAsync(Seat seat)
        {
            _FobumCinemaContext.Seat.Add(seat);
            await _FobumCinemaContext.SaveChangesAsync();
            return seat;
        }

        public async Task UpdateAsync(Seat seat)
        {
            _FobumCinemaContext.Seat.Update(seat);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seat seat)
        {
            _FobumCinemaContext.Seat.Remove(seat);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
