using EShop.Core.Entities;
using EShop.Dtos.Order.Dtos;
using EShop.Dtos.User.Models;

namespace EShop.Core.Domain
{
    public interface IUserService
    {
        Task<User> GetUserAsync(long userId);
        Task<bool> EditUserAsync(User user);
        Task AddAddressAsync(AddAddressModel model, long userId);
    }
}