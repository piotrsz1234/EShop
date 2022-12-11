using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace EShop.Core.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static void Entity<T>(this DbModelBuilder builder, Action<EntityTypeConfiguration<T>> setter) where T : class
        {
            setter.Invoke(builder.Entity<T>());
        }
    }
}