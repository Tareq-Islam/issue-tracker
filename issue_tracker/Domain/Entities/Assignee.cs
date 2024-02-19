using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Assignee
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? IssueId { get; set; }

    public int? UserType { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Issue? Issue { get; set; }

    public virtual User? User { get; set; }
}
