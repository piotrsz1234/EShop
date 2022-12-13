using System.Data.Entity;
using EShop.Core.Entities;
using EShop.Core.Entities.Attributes;
using EShop.Core.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using File = EShop.Core.Entities.File;

namespace EShop.Implementation.EFCore.Contexts
{
    public class MainDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        public MainDbContext(): base("Data Source=.;Initial Catalog=SimpleShop;Trusted_Connection=True")
        {
            
        }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Add(new AttributeToColumnAnnotationConvention<SqlDefaultValueAttribute, string>("SqlDefaultValue", 
                (info, list) => list.Single().DefaultValue));
            
            builder.Entity<Address>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.User).WithMany(e => e.Addresses).HasForeignKey(e => e.UserId);
            });

            builder.Entity<Category>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasOptional(e => e.OwnerCategory).WithMany(e => e.OwnedCategories);
            });

            builder.Entity<File>(entity => {
                entity.HasKey(e => e.Id);
            });

            builder.Entity<Promotion>(entity => {
                entity.HasKey(e => e.Id);
            });

            builder.Entity<ProductPromotion>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Promotion).WithMany(e => e.ProductPromotions).HasForeignKey(e => e.PromotionId);
                entity.HasRequired(e => e.Product).WithMany(e => e.ProductPromotions).HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Order>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Address).WithMany(e => e.Orders).HasForeignKey(e => e.AddressId);
                entity.HasRequired(e => e.User).WithMany(e => e.Orders).HasForeignKey(e => e.UserId);
                entity.HasRequired(e => e.ShippingMethod).WithMany(e => e.Orders).HasForeignKey(e => e.ShippingMethodId);
            });
            
            builder.Entity<OrderProduct>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Order).WithMany(e => e.OrderProduct).HasForeignKey(e => e.OrderId);
                entity.HasRequired(e => e.Product).WithMany(e => e.OrderProduct).HasForeignKey(e => e.ProductId);
            });
            
            builder.Entity<Product>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
            });
            
            builder.Entity<ProductFile>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasRequired(e => e.File).WithMany(e => e.ProductFiles).HasForeignKey(e => e.FileId);
                entity.HasRequired(e => e.Product).WithMany(e => e.ProductFiles).HasForeignKey(e => e.ProductId);
            });
            
            builder.Entity<Role>(entity => {
                entity.HasKey(e => e.Id);
            });
            
            builder.Entity<ShippingMethod>(entity => {
                entity.HasKey(e => e.Id);
            });
            
            builder.Entity<UserRole>(entity => {
                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(builder);
        }
    }
}