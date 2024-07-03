using System;
using System.Collections.Generic;

namespace RedditApi.Models;

public partial class Subreddit
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
