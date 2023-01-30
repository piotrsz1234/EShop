using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public class AddressRepository : RepositoryGenericBase<Address>, IAddressRepository
    {
        public AddressRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}