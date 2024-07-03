using System;
using System.Collections.Generic;

namespace RedditApi.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public int? SubredditId { get; set; }

    public int? UserId { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Subreddit? Subreddit { get; set; }

    public virtual User? User { get; set; }
}
