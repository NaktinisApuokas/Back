using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.API.Models.Dtos.SeatType;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public class NewCinemaHallDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public List<List<SeatTypeDto?>> Matrix { get; set; }
    }
}
