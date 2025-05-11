using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public record SeatTypeDto(
        int Id, 
        [Required] string Name,
        [Required] string? LogoData,
        string? LogoPath,
        decimal DefaultPrice,
        int CinemaCompanyId, 
        int Width, 
        int Height);
}
