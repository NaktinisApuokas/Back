using FobumCinema.API.Models.Dtos.SeatType;

namespace FobumCinema.API.Models.Dtos.Ticket
{
    public record CreateTicketDto(
        string Username,
        List<SeatTypeForTicketsDto> SelectedSeats
    );
}
