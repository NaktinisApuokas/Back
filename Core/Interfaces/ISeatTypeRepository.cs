using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ISeatTypeRepository
    {
        Task<SeatType> GetAsync(int seatTypeID);

        Task<List<SeatType>> GetAllAsync(int cinemaCompanyId);

        Task<SeatType> InsertAsync(SeatType seatType);

        Task UpdateAsync(SeatType hallType);

        Task DeleteAsync(SeatType seatType);
    }
}
