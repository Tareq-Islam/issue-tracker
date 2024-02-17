using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string LoginName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string UserMobileNumber { get; set; } = null!;

    public int? RoleId { get; set; }

    public int? VendorId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Vendor? Vendor { get; set; }
}
