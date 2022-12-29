using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class CategoryRepository : RepositoryGenericBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyCollection<long>> GetAllWithDependentAsync(long categoryId)
        {
            var query = DbContext.Category.Include(x => x.OwnedCategories).Where(x => x.IsDeleted == false && x.Id == categoryId);

            query = query.SelectMany(x => x.OwnedCategories).Union(query);

            return await query.Select(x => x.Id).Distinct().ToArrayAsync();
        }
    }
}