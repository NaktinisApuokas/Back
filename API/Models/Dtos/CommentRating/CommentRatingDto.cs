namespace FobumCinema.API.Models.Dtos.CommentRating
{
    public record CommentRatingDto(int Id, int CommentId, double Score, string Username);
}
