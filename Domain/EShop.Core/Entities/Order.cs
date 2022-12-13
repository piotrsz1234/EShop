using EShop.Core.Entities.Attributes;
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
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public string OrderNumber { get; set; }
        public long UserId { get; set; }
        //public decimal UserDiscount { get; set; }
        public PaymentType PaymentType { get; set; }
        public long ShippingMethodId { get; set; }
        public long AddressId { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }
    }
}