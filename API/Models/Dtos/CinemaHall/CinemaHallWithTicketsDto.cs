using FobumCinema.API.Models.Dtos.Screening;
using FobumCinema.API.Models.Dtos.SeatType;
using System.ComponentModel.DataAnnotations;

namespace FobumCinema.API.Models.Dtos.CinemaHall
{
    public class CinemaHallWithTicketsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public string RowSorting { get; set; }
        public string CollumnSorting { get; set; }
        public List<List<SeatWithTicketsDto?>> Matrix { get; set; }
    }
}
