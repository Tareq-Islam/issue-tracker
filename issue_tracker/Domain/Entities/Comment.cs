using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int? IssueId { get; set; }

    public int? AssigneeId { get; set; }

    public string? Subject { get; set; }

    public string? CommentText { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual Assignee? Assignee { get; set; }

    public virtual Issue? Issue { get; set; }
}
