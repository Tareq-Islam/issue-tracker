using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IssueCauseFindingsMapping
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int CauseFindingId { get; set; }
}
