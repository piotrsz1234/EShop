using EShop.Core.Entities;
using EShop.Dtos.Order.Dtos;

namespace EShop.Core.Domain
{
    public interface IUserService
    {
        Task<User> GetUserAsync(long userId);
        Task<bool> EditUserAsync(User user);
    }
}