using System.ComponentModel;

namespace Domain.Enums;

public enum RoleEnum
{
    [Description("Super Admin")]
    Super_Admin = 1,
    [Description("Admin")]
    Admin = 2,
    [Description("Vendor")]
    Vendor = 3,
    [Description("Operation")]
    Operation = 4
}
