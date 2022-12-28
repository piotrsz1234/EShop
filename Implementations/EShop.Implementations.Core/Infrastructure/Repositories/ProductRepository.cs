using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryGenericBase<Product>, IProductRepository
    {
        public ProductRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}