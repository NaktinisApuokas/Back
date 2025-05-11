namespace FobumCinema.API.Models.Dtos.Screening
{
    public record ScreeningInfoDto(
        int ScreeningId,
        string Time,
        string MovieTitle,
        string MovieTitleEng,
        string CinemaName, 
        string ScreeningDateTime
    );

}
