using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat> GetAsync(int seatID);

        Task<List<Seat>> GetAllAsync(int cinemaHallId);

        Task<Seat> InsertAsync(Seat seat);

        Task UpdateAsync(Seat seat);

        Task DeleteAsync(Seat seat);
    }
}
