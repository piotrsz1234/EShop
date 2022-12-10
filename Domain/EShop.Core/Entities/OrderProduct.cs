using System;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class OrderProduct : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public long Count { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}