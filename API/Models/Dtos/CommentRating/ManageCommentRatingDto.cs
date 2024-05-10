using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CommentRating
{
    public record ManageCommentRatingDto([Required] int CommentId, [Required] double Score, [Required] string Username);
}
