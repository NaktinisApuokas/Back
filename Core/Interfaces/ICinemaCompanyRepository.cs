using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ICinemaCompanyRepository
    {
        Task<IEnumerable<CinemaCompany>> GetAll();
        Task<CinemaCompany> Get(int id);
        Task<CinemaCompany> Create(CinemaCompany cinemaCompany);
        Task<CinemaCompany> Put(CinemaCompany cinemaCompany);
        Task Delete(CinemaCompany cinemaCompany);
    }

}
