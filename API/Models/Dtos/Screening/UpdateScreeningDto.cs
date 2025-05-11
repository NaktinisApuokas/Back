using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Screening
{
    public record UpdateScreeningDto(string Time,
        string Price,
        string Emptyseatnumber,
        string Url,
        string Date,
        [Required] int CinemaHallID
        );
}
