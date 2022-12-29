using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class UserRepository : RepositoryGenericBase<User>, IUserRepository
    {
        public UserRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}