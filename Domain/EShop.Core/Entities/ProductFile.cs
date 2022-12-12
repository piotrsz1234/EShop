using EShop.Core.Entities.Attributes;
using System;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class ProductFile : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public long FileId { get; set; }
        public long ProductId { get; set; }
        
        public virtual File File { get; set; }
        public virtual Product Product { get; set; }
    }
}