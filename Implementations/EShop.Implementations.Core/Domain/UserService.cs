using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;

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
    }
}