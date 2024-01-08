using FobumCinema.Data.Dtos.Screening;

namespace FobumCinema.Data.Dtos.Movie
{
        public record MovieDto(List<ScreeningDto> Screenings, int Id, string Title, string Genre, string Duration, string Img, string Description);
}
