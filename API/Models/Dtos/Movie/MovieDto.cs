using FobumCinema.API.Models.Dtos.Screening;

namespace FobumCinema.API.Models.Dtos.Movie
{
    public record MovieDto(List<ScreeningDto> Screenings, 
        int Id,
        string Title,
        string TitleEng,
        string Genre,
        string Duration,
        string Img,
        string Description,
        string TrailerURL,
        bool IsMarked
        );
}
