using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Review
{
    public record CreateReviewDto([Required] string Text, string Username);

}
