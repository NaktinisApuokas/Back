namespace FobumCinema.API.Models.Dtos.Comment
{
    public record CommentDto(int Id, string Text, string Username, double TotalScore, double Score);
}
