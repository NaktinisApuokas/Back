using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ISeatTypePriceRepository
    {
        Task<SeatTypePrice> GetAsync(int seatTypePriceID);

        Task<List<SeatTypePrice>> GetAllAsync(int movieId);

        Task<SeatTypePrice> InsertAsync(SeatTypePrice seatTypePrice);

        Task UpdateAsync(SeatTypePrice hallType);

        Task DeleteAsync(SeatTypePrice seatTypePrice);
    }
}
