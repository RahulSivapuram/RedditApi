using System;
using System.Collections.Generic;

namespace RedditApi.Models;

public partial class Commentsdetail
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public int? PostId { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedAt { get; set; }
}
