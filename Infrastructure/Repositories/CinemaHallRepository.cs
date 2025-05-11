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

    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public CinemaHallRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }
        
        public async Task<CinemaHall> GetAsync(int cinemaHallId)
        {
            return await _FobumCinemaContext.CinemaHall.FirstOrDefaultAsync(o => o.Id == cinemaHallId);
        }
        public async Task<CinemaHall> GetByScreeningIDAsync(int ScreeningID)
        {
            var Screening = await _FobumCinemaContext.Screening.FirstOrDefaultAsync(o => o.Id == ScreeningID);

            return await _FobumCinemaContext.CinemaHall.FirstOrDefaultAsync(o => o.Id == Screening.CinemaHallId);
        }

        public async Task<List<CinemaHall>> GetAllAsync(int cinemaId)
        {
            return await _FobumCinemaContext.CinemaHall.Where(o => o.CinemaId == cinemaId).ToListAsync();
        }

        public async Task<CinemaHall> InsertAsync(CinemaHall cinemaHall)
        {
            _FobumCinemaContext.CinemaHall.Add(cinemaHall);
            await _FobumCinemaContext.SaveChangesAsync();
            return cinemaHall;
        }

        public async Task UpdateAsync(CinemaHall cinemaHall)
        {
            _FobumCinemaContext.CinemaHall.Update(cinemaHall);
            await _FobumCinemaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CinemaHall cinemaHall)
        {
            _FobumCinemaContext.CinemaHall.Remove(cinemaHall);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
