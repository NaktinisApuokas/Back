namespace FobumCinema.API.Models.Dtos.Cinema
{
    public record UpdateCinemaDto(
        string Name,
        string Address,
        string Img,
        double Lat,
        double Lon
        );
}
