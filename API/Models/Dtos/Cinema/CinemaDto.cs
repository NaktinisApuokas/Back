namespace FobumCinema.API.Models.Dtos.Cinema
{
    public record CinemaDto(int Id,
        string Name,
        string Img,
        string Address,
        string Lat,
        string Lon
    );
}
