using System;

namespace FobumCinema.Core.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfSeats { get; set; }

        public bool HasDisabledSeats { get; set; }

        public int CinemaId { get; set; }

        public int HallTypeId { get; set; }

        public HallType HallType { get; set; }

        public Cinema Cinema { get; set; }

    }
}
