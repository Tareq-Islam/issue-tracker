﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public byte[]? Name { get; set; }

    public string? Description { get; set; }
}
