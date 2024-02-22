using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Issue
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int VendorId { get; set; }

    public int SiteId { get; set; }

    public int PriorityStatus { get; set; }

    public int Status { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<IssueCauseFindingsMapping> IssueCauseFindingsMappings { get; set; } = new List<IssueCauseFindingsMapping>();

    public virtual ICollection<IssueSolutionTagMapping> IssueSolutionTagMappings { get; set; } = new List<IssueSolutionTagMapping>();

    public virtual Vendor Vendor { get; set; } = null!;
    public virtual Site Site { get; set; } = null!;
}
