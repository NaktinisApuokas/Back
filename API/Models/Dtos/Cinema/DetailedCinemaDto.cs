namespace FobumCinema.API.Models.Dtos.Cinema
{
    public record DetailedCinemaDto(int Id, string Name, string Img, string Address, int CinemaHallsCount, bool HasDisabledSeats);

}
