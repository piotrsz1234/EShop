using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
using EShop.Dtos.User.Dtos;
using EShop.Dtos.User.Models;
using EShop.Implementations.Core.Infrastructure.Repositories;

namespace EShop.Implementations.Core.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _addressRepository = addressRepository;
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

        public async Task<IReadOnlyCollection<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync(x => x.IsDeleted == false);

            return users.Select(x => new UserDto() {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                IsAdmin = true,
            }).ToArray();
        }

        public async Task ChangeUserRole(long userId)
        {
            var user = await _userRepository.GetOneAsync(userId);

            if (user.UserRole.Any(x => x.IsDeleted == false && x.Role.IsAdmin)) {
                user.UserRole.First(x => x.IsDeleted == false && x.Role.IsAdmin).IsDeleted = true;
            } else {
                user.UserRole.Add(new UserRole() {
                    RoleId = (await _roleRepository.GetOneAsync(x => x.IsAdmin == true)).Id
                });
            }

            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(long userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            user.IsDeleted = true;
            
            await _userRepository.SaveChangesAsync();
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