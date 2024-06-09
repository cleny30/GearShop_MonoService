using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model.Entity;

public partial class PrndatabaseContext : DbContext
{
    public PrndatabaseContext()
    {
    }

    public PrndatabaseContext(DbContextOptions<PrndatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

    public virtual DbSet<ImportProduct> ImportProducts { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ReceiptProduct> ReceiptProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=UNKEPTHAROLDDPC;Initial Catalog=PRNDatabase_PRN221;uid=sa;pwd=222;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("brand_brand_id_primary");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId)
                .ValueGeneratedNever()
                .HasColumnName("brand_id");
            entity.Property(e => e.BrandLogo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand_logo");
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("brand_name");
            entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Cart");

            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("pro_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Pro).WithMany()
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_pro_id_foreign");

            entity.HasOne(d => d.UsernameNavigation).WithMany()
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_username_foreign");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("category_cate_id_primary");

            entity.ToTable("Category");

            entity.Property(e => e.CateId)
                .ValueGeneratedNever()
                .HasColumnName("cate_id");
            entity.Property(e => e.CateName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cate_name");
            entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");
            entity.Property(e => e.Keyword)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("keyword");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("customer_username_primary");

            entity.ToTable("Customer");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<DeliveryAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("delivery_address_id_primary");

            entity.ToTable("Delivery_address");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.DeliveryAddresses)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("delivery_address_username_foreign");
        });

        modelBuilder.Entity<ImportProduct>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("import_product_receipt_id_primary");

            entity.ToTable("Import_Product");

            entity.Property(e => e.ReceiptId)
                .ValueGeneratedNever()
                .HasColumnName("receipt_id");
            entity.Property(e => e.DateImport).HasColumnName("date_import");
            entity.Property(e => e.Payment).HasColumnName("payment");
            entity.Property(e => e.PersonChange)
                .HasMaxLength(50)
                .HasColumnName("person_change");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manager_id_primary");

            entity.ToTable("Manager");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Column9).HasColumnName("column_9");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Password)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Ssn)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SSN");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("order_order_id_primary");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("order_id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.OrderDes)
                .HasColumnType("text")
                .HasColumnName("order_des");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Manager).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("order_manager_id_foreign");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_username_foreign");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Order_Detail");

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pro_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_order_id_foreign");

            entity.HasOne(d => d.Pro).WithMany()
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_pro_id_foreign");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("product_pro_id_primary");

            entity.ToTable("Product");

            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CateId).HasColumnName("cate_id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");
            entity.Property(e => e.ProDes)
                .HasColumnType("text")
                .HasColumnName("pro_des");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("pro_name");
            entity.Property(e => e.ProPrice).HasColumnName("pro_price");
            entity.Property(e => e.ProQuan).HasColumnName("pro_quan");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_brand_id_foreign");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_cate_id_foreign");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity
                
                .ToTable("Product_Attribute");
            entity.HasKey(e => new { e.ProId, e.Feature });

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Feature)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("feature");
            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");

            entity.HasOne(d => d.Pro).WithMany()
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_attribute_pro_id_foreign");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => new { e.ProId, e.ProImg });
            entity
                
                .ToTable("Product_Image");

            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");
            entity.Property(e => e.ProImg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pro_img");

            entity.HasOne(d => d.Pro).WithMany()
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_image_pro_id_foreign");
        });

        modelBuilder.Entity<ReceiptProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Receipt_Product");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pro_id");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("pro_name");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

            entity.HasOne(d => d.Pro).WithMany()
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_product_pro_id_foreign");

            entity.HasOne(d => d.Receipt).WithMany()
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_product_receipt_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
