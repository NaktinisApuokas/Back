using FobumCinema.Core.Entities;

namespace FobumCinema.Core.Interfaces
{
    public interface IMovieMarkRepository
    {
        Task<MovieMark> GetAsync(int movieMarkId);
        Task<MovieMark> GetByMovieAndNameAsync(int movieId, string name);
        Task<List<MovieMark>> GetAllAsync(string name);
        Task InsertAsync(MovieMark movieMark);
        Task DeleteAsync(MovieMark movieMark);
    }

}
