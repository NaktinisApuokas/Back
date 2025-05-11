namespace FobumCinema.API.Models.Dtos.Screening
{
    public record ScreeningDto(int Id,
        string Time,
        string Emptyseatnumber,
        string Price,
        string Url,
        string Date,
        int CinemaHallId);
}
