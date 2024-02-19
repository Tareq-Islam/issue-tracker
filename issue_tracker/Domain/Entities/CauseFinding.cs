using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CauseFinding
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual ICollection<IssueCauseFindingsMapping> IssueCauseFindingsMappings { get; set; } = new List<IssueCauseFindingsMapping>();
}
