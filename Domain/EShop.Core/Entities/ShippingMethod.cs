﻿using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class ShippingMethod : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }

        public ShippingMethod()
        {
            Orders = new HashSet<Order>();
        }
    }
}