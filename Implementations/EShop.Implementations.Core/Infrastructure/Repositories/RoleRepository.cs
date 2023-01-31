using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryGenericBase<Role>, IRoleRepository
    {
        public RoleRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}