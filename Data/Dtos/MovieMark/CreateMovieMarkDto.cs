using System.ComponentModel.DataAnnotations;

namespace FobumCinema.Data.Dtos.MovieMark
{
    public record CreateMovieMarkDto([Required] string Username);
}
