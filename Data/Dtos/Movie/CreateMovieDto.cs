using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FobumCinema.Data.Dtos.Movie
{
        public record CreateMovieDto([Required] string Title, [Required] string Genre, [Required] string Description);
    
}
