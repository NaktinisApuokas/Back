using FobumCinema.API.Models.Dtos.Screening;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.SeatType
{
    public class SeatTypeForTicketsDto
    {
        public int Id { get; set; }
        public decimal DefaultPrice { get; set; }
        public string location { get; set; }
    }
}
