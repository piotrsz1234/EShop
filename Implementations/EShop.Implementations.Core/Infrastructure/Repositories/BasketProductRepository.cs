using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class BasketProductRepository : RepositoryGenericBase<BasketProduct>, IBasketProductRepository
    {
        public BasketProductRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}