using System.ComponentModel.DataAnnotations;

namespace FobumCinema.Data.Dtos.Screening
{
    public record CreateScreeningDto( [Required] string Time, [Required] string Price, [Required] int Emptyseatnumber);

}
