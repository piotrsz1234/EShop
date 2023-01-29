using EShop.Core.Entities;
using EShop.Core.Extensions;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;
using System.Data.Entity;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class CategoryRepository : RepositoryGenericBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyCollection<long>> GetAllWithDependentAsync(long categoryId)
        {
            var resultList = new List<long>(new [] {categoryId});
            var firstCategory = await DbContext.Category.Include(x => x.OwnedCategories).Where(x => x.IsDeleted == false && x.Id == categoryId).FirstAsync();
            var queue = new Queue<long>(firstCategory.OwnedCategories.Select(x => x.Id).ToList());

            while (queue.Count > 0) {
                long currentId = queue.Dequeue();
                var cat = await DbContext.Category.Include(x => x.OwnedCategories).FirstAsync(x => x.Id == currentId);
                queue.EnqueueRange(cat.OwnedCategories.Select(x => x.Id).ToList());
                resultList.Add(currentId);
            }

            return resultList.Distinct().ToList();
        }

        public IReadOnlyCollection<Category> GetAllNonDepended(long? originalCategory)
        {
            var resultList = new List<long>();

            if (originalCategory != null) {
                var firstCategory = DbContext.Category.Include(x => x.OwnedCategories).First(x => x.IsDeleted == false && x.Id == originalCategory);
                var queue = new Queue<long>(firstCategory.OwnedCategories.Select(x => x.Id).ToList());

                while (queue.Count > 0) {
                    long currentId = queue.Dequeue();
                    var cat = DbContext.Category.Include(x => x.OwnedCategories).First(x => x.Id == currentId);
                    queue.EnqueueRange(cat.OwnedCategories.Select(x => x.Id).ToList());
                    resultList.Add(currentId);
                }
            }

            return DbContext.Category.Where(x => resultList.Any(y => y == x.Id) == false || x.Id == originalCategory).ToList();
        }

        public void ForceUpdate(Category entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}