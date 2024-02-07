using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public int? RoleType { get; set; }
}
