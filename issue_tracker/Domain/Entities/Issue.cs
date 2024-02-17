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
}
