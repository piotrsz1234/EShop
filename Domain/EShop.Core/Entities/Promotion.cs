using EShop.Core.Entities.Attributes;
using EShop.Core.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities
{
    public class Promotion : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        [Range(0, 100, ErrorMessage = "Promotion procent value can't be lower than 0% or higher than 100%")]
        public decimal ProcentValue { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime StartPromotionDate { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime EndPromotionDate { get; set; }


        public virtual ICollection<ProductPromotion> ProductPromotions { get; set; }

        public Promotion()
        {
            ProductPromotions = new HashSet<ProductPromotion>();
        }
    }
}