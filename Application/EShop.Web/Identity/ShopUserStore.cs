using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace EShop.Web.Identity
{
    public class ShopUserStore : IUserStore<User, long>
    {
        private readonly IUserRepository _userRepository;

        public ShopUserStore()
        {
            _userRepository = AutofacConfig.Resolve<IUserRepository>();
        }
        
        public async Task CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true;
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(long userId)
        {
            return await _userRepository.GetOneAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userRepository.GetOneAsync(x => x.UserName == userName);
        }
        
        public void Dispose()
        {
            
        }
    }
}