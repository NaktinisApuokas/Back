namespace FobumCinema.API.Models.Dtos.Screening
{
    public record TicketInfoDto(
        int ScreeningId,
        string Time,
        string MovieTitle,
        string MovieTitleEng,
        string CinemaName, 
        string ScreeningDateTime,
        decimal Price,
        int Row,
        int Col,
        bool IsScanned
    );

}
