using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatTypePrice
{
    public record CreateSeatTypePriceDto([Required] decimal Price, int SeatTypeId, int MovieId);
}
