using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Models
{
    public partial class BikeContext : DbContext
    {
        public BikeContext()
        {
        }

        public BikeContext(DbContextOptions<BikeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admi> Admis { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product1> Product1s { get; set; } = null!;
        public virtual DbSet<User1> User1s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Bike;User Id=sa;Password=!Morning1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admi>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Admi");

                entity.Property(e => e.UserId).HasColumnName("User id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .ValueGeneratedNever()
                    .HasColumnName("Cart id");

                entity.Property(e => e.ProductId).HasColumnName("Product id");

                entity.Property(e => e.UserId).HasColumnName("User id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__cart__Product id__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__cart__User id__4222D4EF");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Supplier Name");
            });

            modelBuilder.Entity<Product1>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("Product1");

                entity.Property(e => e.ProductId).HasColumnName("Product id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("Category id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product name");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Product1s)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Product1__id__36B12243");
            });

            modelBuilder.Entity<User1>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User1");

                entity.Property(e => e.UserId).HasColumnName("User id");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email id");

                entity.Property(e => e.MobileNo).HasColumnName("Mobile No");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
