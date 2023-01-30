using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public class BasketRepository : RepositoryGenericBase<Basket>, IBasketRepository
    {
        public BasketRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}