using EShop.Core.Entities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.Core.Entities.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EShop.Core.Entities
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IEntity
    {
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public bool IsNewsletterReceiver { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}