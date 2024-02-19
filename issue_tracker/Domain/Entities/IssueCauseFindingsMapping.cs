using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IssueCauseFindingsMapping
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int CauseFindingId { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual CauseFinding CauseFinding { get; set; } = null!;

    public virtual Issue Issue { get; set; } = null!;
}
