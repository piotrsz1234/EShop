using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using EShop.Core.Entities;
using EShop.Core.Entities.Attributes;
using EShop.Core.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;

namespace EShop.Implementations.EF.Contexts
{
    public class MainDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<ProductPromotion> ProductPromotion { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductFile> ProductFile { get; set; }
        public DbSet<ShippingMethod> ShippingMethod { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketProduct> BasketProduct { get; set; }

        public MainDbContext() : base("MainDbContext")
        {
            Database.Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.User).WithMany(e => e.Addresses).HasForeignKey(e => e.UserId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOptional(e => e.OwnerCategory).WithMany(e => e.OwnedCategories);
            });

            builder.Entity<File>(entity => { entity.HasKey(e => e.Id); });

            builder.Entity<Promotion>(entity => { entity.HasKey(e => e.Id); });

            builder.Entity<ProductPromotion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Promotion).WithMany(e => e.ProductPromotions)
                    .HasForeignKey(e => e.PromotionId);
                entity.HasRequired(e => e.Product).WithMany(e => e.ProductPromotions).HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Address).WithMany(e => e.Orders).HasForeignKey(e => e.AddressId);
                entity.HasRequired(e => e.User).WithMany(e => e.Orders).HasForeignKey(e => e.UserId)
                    .WillCascadeOnDelete(false);
                entity.HasRequired(e => e.ShippingMethod).WithMany(e => e.Orders)
                    .HasForeignKey(e => e.ShippingMethodId);
            });

            builder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Order).WithMany(e => e.OrderProduct).HasForeignKey(e => e.OrderId);
                entity.HasRequired(e => e.Product).WithMany(e => e.OrderProduct).HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
                entity.HasOptional(e => e.OldVersionProduct).WithMany(e => e.NewVersionsProduct)
                    .HasForeignKey(e => e.OldVersionProductId);
            });

            builder.Entity<ProductFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.File).WithMany(e => e.ProductFiles).HasForeignKey(e => e.FileId);
                entity.HasRequired(e => e.Product).WithMany(e => e.ProductFiles).HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Role>(entity => { entity.HasKey(e => e.Id); });

            builder.Entity<ShippingMethod>(entity => { entity.HasKey(e => e.Id); });

            builder.Entity<UserRole>(entity => {
                entity.HasKey(e => e.Id);
            });

            builder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.User).WithMany(e => e.Baskets).HasForeignKey(e => e.UserId);
            });

            builder.Entity<BasketProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Basket).WithMany(e => e.BasketProducts).HasForeignKey(e => e.BasketId);
            });

            base.OnModelCreating(builder);
        }
    }
}