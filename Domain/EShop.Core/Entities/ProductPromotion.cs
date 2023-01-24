using EShop.Core.Entities.Attributes;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class ProductPromotion : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public long PromotionId { get; set; }
        public long ProductId { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual Product Product { get; set; }
    }
}