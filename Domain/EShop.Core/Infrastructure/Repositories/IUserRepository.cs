using EShop.Core.Entities;

namespace EShop.Core.Infrastructure.Repositories
{
    public interface IUserRepository : IRepositoryGenericBase<User>
    {
        void ForceUpdate(User entity);
    }
}