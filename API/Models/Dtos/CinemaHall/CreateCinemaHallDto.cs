using FobumCinema.API.Models.Dtos.SeatType;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public record CreateCinemaHallDto([Required] string Name,
        string RowSorting,
        string CollumnSorting,
        List<List<SeatTypeDto?>> CellMatrix );

}
