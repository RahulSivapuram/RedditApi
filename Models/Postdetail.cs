using System;
using System.Collections.Generic;

namespace RedditApi.Models;

public partial class Postdetail
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public string? Url { get; set; }

    public string? Subreddit { get; set; }

    public string? Username { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
