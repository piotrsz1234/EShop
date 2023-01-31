using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
using EShop.Implementations.Core.Infrastructure.Repositories;

namespace EShop.Implementations.Core.Domain
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(long userId)
        {
            try
            {
                var user = await _userRepository.GetOneAsync(userId);

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> EditUserAsync(User user)
        {
            try
            {
                User entity = await _userRepository.GetOneAsync(user.Id);
                entity.UserName = user.UserName;
                entity.Email = user.Email;
                _userRepository.ForceUpdate(entity);

                await _userRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}