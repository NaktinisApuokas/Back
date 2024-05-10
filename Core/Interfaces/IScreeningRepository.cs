using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IScreeningRepository
    {
        Task<Screening> GetAsync(int screeningId);
        Task<List<Screening>> GetAllAsync(int movieId);
        Task InsertAsync(Screening screening);
        Task InsertRangeAsync(List<Screening> screenings);
        Task UpdateAsync(Screening screening);
        Task DeleteAsync(Screening screening);
        Task DeleteAllAsync();
    }
}
