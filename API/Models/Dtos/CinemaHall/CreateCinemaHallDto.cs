using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public record CreateCinemaHallDto([Required] string Name, string Description, int NumberOfSeats, bool HasDisabledSeats, [Required] int HallTypeId );

}
