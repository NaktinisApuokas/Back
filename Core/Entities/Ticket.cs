using FobumCinema.API.Models.Dtos.SeatType;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FobumCinema.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public bool IsScanned { get; set; }

        public int ScreeningId { get; set; }

        public string User { get; set; }

        public int SeatTypeId { get; set; }
        public SeatType SeatType { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
