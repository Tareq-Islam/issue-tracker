using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class IssueTrack
{
    public int Id { get; set; }

    public int? CauseId { get; set; }

    public int? SolutionId { get; set; }

    public string? Remark { get; set; }
}
