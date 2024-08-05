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

    public class SeatTypeRepository : ISeatTypeRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public SeatTypeRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<SeatType> GetAsync(int seatTypeId)
        {
            return await _FobumCinemaContext.SeatType.FirstOrDefaultAsync(o => o.Id == seatTypeId);
        }

        public async Task<List<SeatType>> GetAllAsync(int cinemaCompanyId)
        {
            return await _FobumCinemaContext.SeatType.Where(o => o.CinemaCompanyId == cinemaCompanyId).ToListAsync();
        }

        public async Task<SeatType> InsertAsync(SeatType seatType)
        {
            _FobumCinemaContext.SeatType.Add(seatType);
            await _FobumCinemaContext.SaveChangesAsync();
            return seatType;
        }

        public async Task UpdateAsync(SeatType seatType)
        {
            _FobumCinemaContext.SeatType.Update(seatType);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SeatType seatType)
        {
            _FobumCinemaContext.SeatType.Remove(seatType);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
