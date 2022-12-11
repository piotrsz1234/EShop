using System;
using EShop.Core.Entities.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EShop.Core.Entities
{
    public class Role : IdentityRole<long, UserRole>, IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public bool IsAdmin { get; set; }
    }
}