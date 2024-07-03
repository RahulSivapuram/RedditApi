namespace RedditApi.helpers
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
