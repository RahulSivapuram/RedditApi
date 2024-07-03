using System;
using System.Collections.Generic;

namespace RedditApi.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PostId { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
