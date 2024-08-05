using System;

namespace FobumCinema.Core.Entities
{
    public class HallType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CinemaCompanyId { get; set; }

        public CinemaCompany CinemaCompany { get; set; }
    }
}
