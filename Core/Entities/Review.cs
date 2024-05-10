using System;

namespace FobumCinema.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string Username { get; set; }
    }
}
