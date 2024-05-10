using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Cinema
{
    public record CreateCinemaDto([Required] string Name, string Img, [Required] string Address);
}
