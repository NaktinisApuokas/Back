using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Screening
{
    public record CreateScreeningDto([Required] string Time, [Required] string Price, [Required] string Emptyseatnumber, string Url);

}
