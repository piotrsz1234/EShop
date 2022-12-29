using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class BasketService : IBasketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBasketProductRepository _basketProductRepository;

        public BasketService(IUserRepository userRepository, IBasketProductRepository basketProductRepository)
        {
            _userRepository = userRepository;
            _basketProductRepository = basketProductRepository;
        }

        public async Task<bool> AddProductToBasket(long userId, long productId, int count)
        {
            try {
                var user = await _userRepository.GetOneAsync(userId, x => x.Baskets);

                if (user is null) return false;

                var basket = user.Baskets.FirstOrDefault(x => x.IsDeleted == false) ?? await AddBasketForUserAsync(user);

                if (basket.BasketProducts.Any(x => x.IsDeleted == false && x.ProductId == productId)) {
                    basket.BasketProducts.First(x => x.IsDeleted == false && x.ProductId == productId).Count += count;
                } else {
                    basket.BasketProducts.Add(new BasketProduct() {
                        ProductId = productId,
                        Count = count
                    });
                }

                await _userRepository.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> EditBasketAsync(long userId, long productId, int count)
        {
            try {
                var user = await _userRepository.GetOneAsync(userId, x => x.Baskets);

                var basket = user?.Baskets.FirstOrDefault(x => x.IsDeleted == false);

                var item = basket?.BasketProducts.FirstOrDefault(x => x.IsDeleted == false && x.ProductId == productId);

                if (item is null) return false;

                if (count == 0) 
                    _basketProductRepository.Remove(item);
                else {
                    item.Count = count;
                    item.ModificationDateUtc = DateTime.UtcNow;
                }
                
                await _userRepository.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }
        
        private async Task<Basket> AddBasketForUserAsync(User user)
        {
            var basket = new Basket();
            user.Baskets.Add(basket);

            await _userRepository.SaveChangesAsync();
            
            return basket;
        }
    }
}