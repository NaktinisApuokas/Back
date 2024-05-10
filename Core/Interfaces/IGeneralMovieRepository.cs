using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IGeneralMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
    }
}
