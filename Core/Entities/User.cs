using Microsoft.AspNetCore.Identity;

namespace FobumCinema.Core.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? AdditionalInfo { get; set; }
    }
}
