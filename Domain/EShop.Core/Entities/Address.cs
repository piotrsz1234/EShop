using EShop.Core.Entities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Address : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Address()
        {
            Orders = new HashSet<Order>();
        }
    }
}