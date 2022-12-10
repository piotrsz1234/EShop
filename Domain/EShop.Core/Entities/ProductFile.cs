using System;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class ProductFile : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public long FileId { get; set; }
        public long ProductId { get; set; }
        
        public virtual File File { get; set; }
        public virtual Product Product { get; set; }
    }
}