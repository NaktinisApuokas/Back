using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.HallType
{
    public record HallTypeDto(int Id, [Required] string Name, string Description, int CinemaCompanyId);
}
