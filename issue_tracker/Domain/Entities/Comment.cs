using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int? IssueTrackId { get; set; }

    public int? AssigneeId { get; set; }

    public string? Subject { get; set; }

    public string? Comment1 { get; set; }

    public DateTime? LogTime { get; set; }
}
