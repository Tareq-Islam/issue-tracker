using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Assignee
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? IssueId { get; set; }

    public int? UserType { get; set; }
}
