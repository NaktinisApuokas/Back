using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatTypePrice
{
    public record UpdateSeatTypePriceDto([Required] decimal Price, int SeatTypeId, int MovieId);
}
