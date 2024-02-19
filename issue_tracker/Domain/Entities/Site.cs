using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Site
{
    public int Id { get; set; }

    public string? SiteName { get; set; }

    public string? Description { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }
}
