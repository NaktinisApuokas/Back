using FobumCinema.API.Models.Dtos.Cinema;
using FobumCinema.API.Models.Dtos.Screening;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record GeneralMovieDto(List<ScreeningDto> Screenings, CinemaDto Cinema, int Id, string Title, string Genre, string Duration, string Img, string Description);
}
