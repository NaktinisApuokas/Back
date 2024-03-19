namespace FobumCinema.Data.Entities
{
    public class CommentRating
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public double Score { get; set; }
        public string Username { get; set; }
    }
}
