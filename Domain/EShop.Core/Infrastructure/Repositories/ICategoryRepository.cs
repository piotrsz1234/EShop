using EShop.Core.Entities;

namespace EShop.Core.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepositoryGenericBase<Category>
    {
        Task<IReadOnlyCollection<long>> GetAllWithDependentAsync(long categoryId);
    }
}