using FobumCinema.API.Models.Dtos.SeatType;

namespace FobumCinema.API.Models.Dtos.Screening
{
    public class ScreeningSeatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public List<List<SeatTypeDto?>> Matrix { get; set; }
    }
}
