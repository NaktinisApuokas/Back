using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetAsync(int movieId);

        Task<List<Movie>> GetAllAsync(int cinemaId); 

        Task<List<Movie>> GetAllUpcomingAsync();

        Task<List<Movie>> GetAllAsync();
        
        Task<Movie> GetByCinemaIdAndTitleAsync(int cinemaId, string title);

        Task<Movie> InsertAsync(Movie movie);

        Task UpdateAsync(Movie movie);

        Task DeleteAsync(Movie movie);
    }
}
