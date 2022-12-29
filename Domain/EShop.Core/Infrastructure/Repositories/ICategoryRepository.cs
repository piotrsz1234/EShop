using EShop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepositoryGenericBase<Category>
    {
        Task<IReadOnlyCollection<long>> GetAllWithDependentAsync(long categoryId);
    }
}