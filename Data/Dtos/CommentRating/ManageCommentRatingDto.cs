using System.ComponentModel.DataAnnotations;

namespace FobumCinema.Data.Dtos.CommentRating
{
    public record CreateMovieMarkDto([Required] int CommentId, [Required] double Score, [Required] string Username);
}
