using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Comment
{
    public record CreateCommentDto([Required] string Text, string Username);

}
