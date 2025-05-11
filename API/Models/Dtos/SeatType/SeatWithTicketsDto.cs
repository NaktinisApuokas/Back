using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public class SeatWithTicketsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoData { get; set; }
        public string? LogoPath { get; set; }
        public decimal DefaultPrice { get; set; }
        public int Width { get; set; }
        public bool IsTaken { get; set; }
        public bool IsReserved { get; set; }

        public SeatWithTicketsDto() { }

        public SeatWithTicketsDto(int id, string name, string? logoData, string? logoPath,
            decimal defaultPrice, int width, bool isTaken, bool isReserved)
        {
            Id = id;
            Name = name;
            LogoData = logoData;
            LogoPath = logoPath;
            DefaultPrice = defaultPrice;
            Width = width;
            IsTaken = isTaken;
            IsReserved = isReserved;
        }
    }
}
