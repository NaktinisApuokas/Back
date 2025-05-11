namespace FobumCinema.Core.Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Address { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public int CinemaCompanyID { get; set; }
    }
}
