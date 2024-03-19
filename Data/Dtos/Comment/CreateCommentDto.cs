using System.ComponentModel.DataAnnotations;

namespace FobumCinema.Data.Dtos.Comment
{
    public record CreateCommentDto( [Required] string Text, string Username);

}
