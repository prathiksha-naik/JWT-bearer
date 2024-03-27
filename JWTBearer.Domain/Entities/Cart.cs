using System;
using System.Collections.Generic;

namespace JWTBearer.Domain.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int Quantity { get; set; }

    public int? UserId { get; set; }

    public int? ClothItemId { get; set; }

    public int? SizeId { get; set; }

    public int? WishListId { get; set; }

    public virtual ClothItem? ClothItem { get; set; }

    public virtual SizeVariant? Size { get; set; }

    public virtual User? User { get; set; }

    public virtual Wishlist? WishList { get; set; }
}
