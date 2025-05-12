using FobumCinema.API.Models.Dtos.Cinema;
using FobumCinema.API.Models.Dtos.Screening;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record GeneralMovieDto(
        string CinemaName,
        string CinemaId,
        int Id,
        string Title,
        string TitleEng,
        string Genre,
        string Duration,
        string Img,
        string Description,
        string TrailerURL
    );
}
