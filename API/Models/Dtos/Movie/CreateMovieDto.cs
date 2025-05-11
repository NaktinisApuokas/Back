using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record CreateMovieDto([Required] string Title,
        [Required] string Genre,
        string Duration,
        string Img,
        [Required] string Description,
        string TrailerURL,
        string TitleEng
        );

}
