using JWTBearer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTBearer.Infrastructure;

public partial class ClothStoreContext : DbContext
{
    public ClothStoreContext()
    {
    }

    public ClothStoreContext(DbContextOptions<ClothStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ClothCategory> ClothCategories { get; set; }

    public virtual DbSet<ClothItem> ClothItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SizeVariant> SizeVariants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F3BE643DAEF6");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797CA8BEC8E");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WishListId).HasColumnName("WishListID");

            entity.HasOne(d => d.ClothItem).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ClothItemId)
                .HasConstraintName("FK__Cart__ClothItemI__3F466844");

            entity.HasOne(d => d.Size).WithMany(p => p.Carts)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__Cart__SizeID__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserID__3E52440B");

            entity.HasOne(d => d.WishList).WithMany(p => p.Carts)
                .HasForeignKey(d => d.WishListId)
                .HasConstraintName("FK__Cart__WishListID__412EB0B6");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B521D65E6");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClothCategory>(entity =>
        {
            entity.HasKey(e => e.ClothCategoryId).HasName("PK__ClothCat__E5E9BD1AF020D23C");

            entity.ToTable("ClothCategory");

            entity.Property(e => e.ClothCategoryId).HasColumnName("ClothCategoryID");
            entity.Property(e => e.ClothCategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClothItem>(entity =>
        {
            entity.HasKey(e => e.ClothItemId).HasName("PK__ClothIte__ED72E6987415E1A8");

            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ClothCategoryId).HasColumnName("ClothCategoryID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.ClothItems)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__ClothItem__Brand__2E1BDC42");

            entity.HasOne(d => d.Category).WithMany(p => p.ClothItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__ClothItem__Categ__2C3393D0");

            entity.HasOne(d => d.ClothCategory).WithMany(p => p.ClothItems)
                .HasForeignKey(d => d.ClothCategoryId)
                .HasConstraintName("FK__ClothItem__Cloth__2D27B809");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFA6723DC8");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__33D4B598");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A19E367003");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ClothItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ClothItemId)
                .HasConstraintName("FK__OrderItem__Cloth__37A5467C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__36B12243");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AED8BDB06E");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.Comment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DatePosted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ClothItem).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ClothItemId)
                .HasConstraintName("FK__Reviews__ClothIt__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__45F365D3");
        });

        modelBuilder.Entity<SizeVariant>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__SizeVari__83BD095AA8BB9DB4");

            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.ClothItem).WithMany(p => p.SizeVariants)
                .HasForeignKey(d => d.ClothItemId)
                .HasConstraintName("FK__SizeVaria__Cloth__30F848ED");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC9D35F250");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK__Wishlist__233189CB93E729AD");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("WishlistID");
            entity.Property(e => e.ClothItemId).HasColumnName("ClothItemID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ClothItem).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ClothItemId)
                .HasConstraintName("FK__Wishlist__ClothI__3B75D760");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Wishlist__UserID__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
