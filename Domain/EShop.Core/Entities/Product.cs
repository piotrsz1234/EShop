using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Product : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsInTrash { get; set; }
        public bool IsHidden { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }
}