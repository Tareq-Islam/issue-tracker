using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Vendor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateTime CreationTime { get; set; }

    public int IsDeleted { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
