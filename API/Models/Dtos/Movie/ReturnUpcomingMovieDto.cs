using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record ReturnUpcomingMovieDto(
        int id,
        string Title,
        string TitleEng,
        string TrailerURL,
        string Genre, 
        string Duration, 
        string Img, 
        string Description,
        string Date,
        int IsUpcoming,
        int CinemaId
        );

}
