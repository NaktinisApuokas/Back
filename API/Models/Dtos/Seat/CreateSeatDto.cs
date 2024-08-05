using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Seat
{
    public record CreateSeatDto([Required] int Name, [Required] int LogoPath, decimal DefaultPrice, int SeatTypeId, int CinemaCompanyId);
}
