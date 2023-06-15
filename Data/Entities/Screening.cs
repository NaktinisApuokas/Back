using System;

namespace FobumCinema.Data.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int Emptyseatnumber { get; set; }
        public string Price { get; set; }
        public string Url { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
