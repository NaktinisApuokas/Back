using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record UpcomingMovieDto([Required] string Title,
        string TitleEng,
        [Required] string Genre, 
        string Duration, 
        string Img, 
        [Required] string Description,
        string Date,
        string TrailerURL,
        int IsUpcoming = 1,
        int CinemaId = 3007
        );

}
