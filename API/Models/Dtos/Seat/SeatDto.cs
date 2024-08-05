using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Seat
{
    public record SeatDto(int Id, [Required] int Name, [Required] int LogoPath, decimal DefaultPrice, int SeatTypeId, int CinemaCompanyId);
}
