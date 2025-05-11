using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface ICinemaHallRepository
    {
        Task<CinemaHall> GetAsync(int cinemaHallID);

        Task<CinemaHall> GetByScreeningIDAsync(int ScreeningID);

        Task<List<CinemaHall>> GetAllAsync(int cinemaId);

        Task<CinemaHall> InsertAsync(CinemaHall cinemaHall);

        Task UpdateAsync(CinemaHall cinemaHall);

        Task DeleteAsync(CinemaHall cinemaHall);
    }
}
