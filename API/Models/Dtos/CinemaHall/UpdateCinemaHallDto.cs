using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public record UpdateCinemaHallDto([Required] string Name, string Description, int NumberOfSeats, bool HasDisabledSeats, [Required] int HallTypeId);

}
