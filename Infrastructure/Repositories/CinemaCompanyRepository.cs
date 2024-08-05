using System.Collections.Generic;
using System.Threading.Tasks;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FobumCinema.Infrastructure.Repositories
{
    public class CinemaCompanyRepository : ICinemaCompanyRepository
    {
        private readonly FobumCinemaContext _FobumCinemaContext;

        public CinemaCompanyRepository(FobumCinemaContext fobumCinemaContext)
        {
            _FobumCinemaContext = fobumCinemaContext;
        }

        public async Task<IEnumerable<CinemaCompany>> GetAll()
        {
            return await _FobumCinemaContext.CinemaCompany.ToListAsync();
        }

        public async Task<CinemaCompany> Get(int id)
        {
            return await _FobumCinemaContext.CinemaCompany.FirstOrDefaultAsync(o => o.CinemaCompanyID == id);
        }

        public async Task<CinemaCompany> Create(CinemaCompany cinemaCompany)
        {
            _FobumCinemaContext.CinemaCompany.Add(cinemaCompany);
            await _FobumCinemaContext.SaveChangesAsync();

            return cinemaCompany;
        }

        public async Task<CinemaCompany> Put(CinemaCompany cinemaCompany)
        {
            _FobumCinemaContext.CinemaCompany.Update(cinemaCompany);
            await _FobumCinemaContext.SaveChangesAsync();

            return cinemaCompany;
        }

        public async Task Delete(CinemaCompany cinemaCompany)
        {
            _FobumCinemaContext.CinemaCompany.Remove(cinemaCompany);
            await _FobumCinemaContext.SaveChangesAsync();
        }
    }
}
