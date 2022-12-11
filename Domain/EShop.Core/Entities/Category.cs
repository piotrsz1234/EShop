using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Category : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        public long? OwnerCategoryId { get; set; }

        public virtual Category OwnerCategory { get; set; }
        public virtual ICollection<Category> OwnedCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            OwnedCategories = new HashSet<Category>();
            Products = new HashSet<Product>();
        }
    }
}