using EShop.Core.Entities.Attributes;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Basket : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }

        public long UserId { get; set; }
        
        public virtual User User { get; set; }
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }

        public Basket()
        {
            BasketProducts = new HashSet<BasketProduct>();
        }
    }
}