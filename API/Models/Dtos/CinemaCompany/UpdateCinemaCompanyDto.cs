using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaCompany
{
    public record UpdateCinemaCompanyDto([Required] string Name, string Description, [Required] string Img, string CompanyUrl);
}
