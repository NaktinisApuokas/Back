using System;

namespace FobumCinema.Core.Entities
{
    public class SeatTypePrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int SeatTypeId { get; set; }

        public SeatType SeatType { get; set; }

        public int ScreeningId { get; set; }

        public Screening Screening { get; set; }
    }
}
