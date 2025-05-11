using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public record CreatedSeatTypeDto(int Id,
        [Required] string Name,
        string? LogoPath,
        decimal DefaultPrice,
        int CinemaCompanyId,
        int Width,
        int Height);
}
