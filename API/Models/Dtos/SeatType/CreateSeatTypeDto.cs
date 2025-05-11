using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public record CreateSeatTypeDto([Required] string Name,
        [Required] IFormFile Logo,
        int Width,
        decimal DefaultPrice);
}
