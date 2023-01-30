using EShop.Dtos.Basket.Dtos;

namespace EShop.Core.Domain
{
    public interface IBasketService
    {
        Task<bool> AddProductToBasketAsync(long userId, long productId, int count);
        Task<bool> EditBasketAsync(long userId, long productId, int count);
        Task<bool> RemoveItemFromBasketAsync(long userId, long productId, bool onlyOne = false);
        Task<IReadOnlyCollection<BasketProductDto>> GetBasketItemsAsync(long userId);
    }
}