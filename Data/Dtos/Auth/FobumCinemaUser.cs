using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FobumCinema.Data.Dtos.Auth
{
    public class FobumCinemaUser : IdentityUser
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
