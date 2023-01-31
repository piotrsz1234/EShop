using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;
using System.Data.Entity;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class UserRepository : RepositoryGenericBase<User>, IUserRepository
    {
        public UserRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public void ForceUpdate(User entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}