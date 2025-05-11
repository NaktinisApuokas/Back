using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public record UpdateSeatTypeDto([Required] string Name,
        [Required] string LogoPath,
        decimal DefaultPrice,
        int Width,
        int Height
        );

}
