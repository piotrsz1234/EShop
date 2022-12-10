using System;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class Address : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}