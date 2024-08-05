using System;

namespace FobumCinema.Core.Entities
{
    public class SeatType
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LogoPath { get; set; }

        public decimal DefaultPrice { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int CinemaCompanyId { get; set; }

        public CinemaCompany? CinemaCompany { get; set; }

    }
}
