using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Ticket
{
    public record TicketDto(int Id,
        [Required] decimal Price,
        int SeatTypeId,
        int ScreeningId);
}
