using System.ComponentModel.DataAnnotations;

namespace FobumCinema.Data.Dtos.Review
{
    public record CreateReviewDto( [Required] string Text, string Username);

}
