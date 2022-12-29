using EShop.Core.Entities;
using Microsoft.AspNet.Identity;

namespace EShop.Web.Identity
{
    public class ShopUserValidator : UserValidator<User,long>
    {
        public ShopUserValidator(UserManager<User, long> manager) : base(manager)
        {
        }
    }
}