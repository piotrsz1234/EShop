using EShop.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.Web.Identity
{
    public class ShopSignInManager : SignInManager<User, long>
    {
        public ShopSignInManager(UserManager<User, long> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return GenerateUserIdentityAsync(user, UserManager);
        }

        public static ShopSignInManager Create(IdentityFactoryOptions<ShopSignInManager> options, IOwinContext context)
        {
            return new ShopSignInManager(context.GetUserManager<ShopUserManager>(), context.Authentication);
        }

        private async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, UserManager<User, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}