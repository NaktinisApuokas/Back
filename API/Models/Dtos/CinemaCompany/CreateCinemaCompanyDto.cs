using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaCompany
{
    public record CreateCinemaCompanyDto([Required] string Name, [Required] string Img, string Description, string CompanyUrl);
}
