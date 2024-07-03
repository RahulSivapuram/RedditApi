namespace RedditApi.helpers
{
    public class SubRedditDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; } = default(DateTime?);
    }
}
