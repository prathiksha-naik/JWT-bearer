﻿using System;
using System.Collections.Generic;

namespace JWTBearer.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ClothItem> ClothItems { get; set; } = new List<ClothItem>();
}
