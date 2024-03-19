using FobumCinema.Data.Dtos.Auth;
using System;

namespace FobumCinema.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string Username { get; set; }
    }
}
