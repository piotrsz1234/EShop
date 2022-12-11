using System.Data.Entity;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EShop.Implementation.EFCore.Contexts
{
    public class MainDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Orders).WithRequired(e => e.Address);
                entity.HasRequired(e => e.User).WithMany(e => e.Addresses);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOptional(e => e.OwnerCategory).WithMany(e => e.OwnedCategories);
            });
        }
    }
}