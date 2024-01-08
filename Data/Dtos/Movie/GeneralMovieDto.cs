
using FobumCinema.Data.Dtos.Cinema;
using FobumCinema.Data.Dtos.Screening;

namespace FobumCinema.Data.Dtos.Movie
{
        public record GeneralMovieDto(List<ScreeningDto> Screenings, CinemaDto Cinema, int Id, string Title, string Genre, string Duration, string Img, string Description);
}
