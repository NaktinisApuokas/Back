using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FobumCinema.Core.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Emptyseatnumber { get; set; }
        public string Price { get; set; }
        public string Url { get; set; }
        public int MovieId { get; set; }
        public int CinemaHallId { get; set; }
        public Movie Movie { get; set; }
    }
}
