﻿using System;
using System.Collections.Generic;

namespace JWTBearer.Domain.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime DatePosted { get; set; }

    public int? UserId { get; set; }

    public int? ClothItemId { get; set; }

    public virtual ClothItem? ClothItem { get; set; }

    public virtual User? User { get; set; }
}
