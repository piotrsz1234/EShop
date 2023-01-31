using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
using EShop.Dtos.User.Models;
using EShop.Implementations.Core.Infrastructure.Repositories;

namespace EShop.Implementations.Core.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

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

        public async Task AddAddressAsync(AddAddressModel model, long userId)
        {
            var user = await _userRepository.GetOneAsync(userId);

            await _addressRepository.AddAsync(new Address() {
                UserId = userId,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                Name = $"{user.FirstName} {user.LastName}",
                ZipCode = model.ZipCode,
                PhoneNumber = user.PhoneNumber,
            });

            await _addressRepository.SaveChangesAsync();
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