using System.Threading.Tasks;
using EShop.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace EShop.Web.Identity
{
    public class ShopUserManager : UserManager<User, long>
    {
        private readonly ShopUserStore _store;

        public ShopUserManager(ShopUserStore store) : base(store)
        {
            _store = store;
        }
    
        public static ShopUserManager Create(IdentityFactoryOptions<ShopUserManager> options, IOwinContext context) 
        {
            var manager = new ShopUserManager(new ShopUserStore());
            // Configure validation logic for usernames
            manager.UserValidator = new ShopUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<User, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public async Task AddUserAsync(User user)
        {
            await (Store as ShopUserStore).CreateAsync(user);
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var user = await Store.FindByNameAsync(userName);

            if (user is null) return null;

            if (user.PasswordHash != password) return null;

            return user;
        }
    }
}