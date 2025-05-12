using System.Collections.Generic;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public CinemaRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<IEnumerable<Cinema>> GetAll()
        {
            return await _FobumCinemaContext.Cinema.ToListAsync();
        }

        public async Task<IEnumerable<Cinema>> GetByCity(string city)
        {

            if (city.ToLower().Contains("kiti"))
            {
                return await _FobumCinemaContext.Cinema.Where(o => !o.Address.Contains("Vilnius") &&
                !o.Address.Contains("Kaunas") &&
                !o.Address.Contains("Klaipėda") &&
                !o.Address.Contains("Šiauliai")
                ).ToListAsync();

            }
            else
            {
                return await _FobumCinemaContext.Cinema.Where(o => o.Address.Contains(city)).ToListAsync();
            }
        }

        public async Task<Cinema> Get(int id)
        {
            return await _FobumCinemaContext.Cinema.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Cinema> Create(Cinema cinema)
        {
            _FobumCinemaContext.Cinema.Add(cinema);
            await _FobumCinemaContext.SaveChangesAsync();

            return cinema;
        }

        public async Task<Cinema> Put(Cinema cinema)
        {
            _FobumCinemaContext.Cinema.Update(cinema);
            await _FobumCinemaContext.SaveChangesAsync();

            return cinema;
        }

        public async Task Delete(Cinema cinema)
        {
            _FobumCinemaContext.Cinema.Remove(cinema);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
