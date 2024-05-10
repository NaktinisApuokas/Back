using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.MovieMark
{
    public record CreateMovieMarkDto([Required] string Username);
}
