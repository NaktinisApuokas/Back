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

    public class HallTypeRepository : IHallTypeRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public HallTypeRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<HallType> GetAsync(int hallTypeId)
        {
            return await _FobumCinemaContext.HallType.FirstOrDefaultAsync(o => o.Id == hallTypeId);
        }

        public async Task<List<HallType>> GetAllAsync(int cinemaCompanyId)
        {
            return await _FobumCinemaContext.HallType.Where(o => o.CinemaCompanyId == cinemaCompanyId).ToListAsync();
        }

        public async Task<HallType> InsertAsync(HallType hallType)
        {
            _FobumCinemaContext.HallType.Add(hallType);
            await _FobumCinemaContext.SaveChangesAsync();
            return hallType;
        }

        public async Task UpdateAsync(HallType hallType)
        {
            _FobumCinemaContext.HallType.Update(hallType);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(HallType hallType)
        {
            _FobumCinemaContext.HallType.Remove(hallType);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
