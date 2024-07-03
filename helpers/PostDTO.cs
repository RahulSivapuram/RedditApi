namespace RedditApi.helpers
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Url { get; set; }

        public int? SubredditId { get; set; }

        public int? UserId { get; set; }

        public string? Body { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
