namespace FobumCinema.API.Models.Dtos.Payment
{
    public class PaymentRequestDto
    {
        public List<SeatDto> Seats { get; set; }

        public int ScreeningId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieTitleEng { get; set; }
        public string CinemaName { get; set; }
        public string ScreeningDateTime { get; set; }
    }
}
