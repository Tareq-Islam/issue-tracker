using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IssueSolutionTagMapping
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int SolutionTagId { get; set; }
}
