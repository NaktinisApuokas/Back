using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.Auth
{
    public record RegisterDto([Required] string UserName, [EmailAddress][Required] string Email, [Required] string Password);
}
