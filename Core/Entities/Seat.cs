using System;

namespace FobumCinema.Core.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public int? RowIndex { get; set; }

        public int? ColIndex { get; set; }

        public int SeatTypeId { get; set; }

        public SeatType? SeatType { get; set; }

        public int CinemaHallId { get; set; }

        public CinemaHall? CinemaHall { get; set; }
    }
}
