﻿namespace RedditApi.helpers
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? PostId { get; set; }

        public string? Body { get; set; }

        public DateTime? CreatedAt { get; set; } = default(DateTime?);
    }
}
