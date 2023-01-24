using EShop.Core.Entities.Attributes;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class BasketProduct : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }

        public long BasketId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}