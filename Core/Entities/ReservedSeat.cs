using System;

namespace FobumCinema.Core.Entities
{
    public class ReservedSeat
    {
        public int Id { get; set; }

        public int ScreeningId { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public DateTime ReservedAt { get; set; }

        public string? SessionId { get; set; }
    }
}
