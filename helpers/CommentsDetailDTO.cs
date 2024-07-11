namespace RedditApi.helpers
{
    public class CommentsDetailDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public int? PostId { get; set; }
        public string? Body { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
