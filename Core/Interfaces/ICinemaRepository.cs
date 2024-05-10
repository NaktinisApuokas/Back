using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<Cinema>> GetAll();
        Task<Cinema> Get(int id);
        Task<Cinema> Create(Cinema cinema);
        Task<Cinema> Put(Cinema cinema);
        Task Delete(Cinema cinema);
    }

}
