using EShop.Core.Entities.Attributes;
using EShop.Core.Entities.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace EShop.Core.Entities
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IEntity, IUser<long>
    {
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public bool IsNewsletterReceiver { get; set; }
        public decimal UserDiscount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

        public User()
        {
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
            Baskets = new HashSet<Basket>();
            UserRole = new HashSet<UserRole>();
        }
    }
}