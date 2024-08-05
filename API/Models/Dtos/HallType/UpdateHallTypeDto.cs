using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.HallType
{
    public record UpdateHallTypeDto([Required] string Name, string Description);

}
