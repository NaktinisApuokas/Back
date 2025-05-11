namespace FobumCinema.API.Models.Dtos.Cinema
{
    public record DetailedCinemaDto(int Id,
        string Name,
        string Img,
        string Address,
        string Lat,
        string Lon,
        int CinemaHallsCount,
        bool HasDisabledSeats
        );

}
