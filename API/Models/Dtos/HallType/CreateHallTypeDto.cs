using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.HallType
{
    public record CreateHallTypeDto([Required] string Name, string Description, int CinemaCompanyId);

}
