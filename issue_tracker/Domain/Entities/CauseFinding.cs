using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CauseFinding
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
