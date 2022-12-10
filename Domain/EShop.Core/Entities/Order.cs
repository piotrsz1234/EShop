using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Enums;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Order : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public string OrderNumber { get; set; }
        public long UserId { get; set; }
        public decimal UserDiscount { get; set; }
        public PaymentType PaymentType { get; set; }
        public long ShippingMethodId { get; set; }

        public virtual User User { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}