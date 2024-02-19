using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IssueSolutionTagMapping
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int SolutionTagId { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual Issue Issue { get; set; } = null!;

    public virtual SolutionTag SolutionTag { get; set; } = null!;
}
