﻿using System;
using System.Collections.Generic;

namespace JWTBearer.Domain.Entities;

public partial class ClothCategory
{
    public int ClothCategoryId { get; set; }

    public string ClothCategoryName { get; set; } = null!;

    public virtual ICollection<ClothItem> ClothItems { get; set; } = new List<ClothItem>();
}
