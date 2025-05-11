using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Seat
{
    public record UpdateSeatDto([Required] int Name, [Required] string LogoPath, decimal DefaultPrice, int SeatTypeId, int CinemaCompanyId);

}
