using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public class ShippingMethodRepository : RepositoryGenericBase<ShippingMethod>, IShippingMethodRepository
    {
        public ShippingMethodRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}