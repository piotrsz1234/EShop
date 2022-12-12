using EShop.Core.Entities.Attributes;
using System;
using EShop.Core.Entities.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities
{
    public class UserLogin : IdentityUserLogin<long>, IEntity
    {
        [Key]
        public long Id { get; set; }

        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
    }
}