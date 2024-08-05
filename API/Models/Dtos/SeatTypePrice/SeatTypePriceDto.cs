using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatTypePrice
{
    public record SeatTypePriceDto(int Id, [Required] decimal Price, int SeatTypeId, int MovieId);
}
