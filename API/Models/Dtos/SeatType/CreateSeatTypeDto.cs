using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public record CreateSeatTypeDto([Required] string Name, [Required] string LogoPath, decimal DefaultPrice, int CinemaCompanyId, int Width, int Height);
}
