namespace EShop.Core.Domain
{
    public interface IBasketService
    {
        Task<bool> AddProductToBasket(long userId, long productId, int count);
        Task<bool> EditBasketAsync(long userId, long productId, int count);
    }
}