using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IHallTypeRepository
    {
        Task<HallType> GetAsync(int hallTypeID);

        Task<List<HallType>> GetAllAsync(int cinemaCompanyId);

        Task<HallType> InsertAsync(HallType hallType);

        Task UpdateAsync(HallType hallType);

        Task DeleteAsync(HallType hallType);
    }
}
