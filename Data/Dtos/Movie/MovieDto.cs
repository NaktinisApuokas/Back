using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FobumCinema.Data.Dtos.Movie
{
        public record MovieDto(int Id, string Title, string Genre, string Description);
}
