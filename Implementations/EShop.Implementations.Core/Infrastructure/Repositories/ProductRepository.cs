using System.Data.Entity;
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

        public async Task<List<Product>> GetAllFromCategoryAsync(long categoryId)
        {
            var query = DbContext.Category.Include(x => x.OwnedCategories)
                .Where(x => x.IsDeleted == false && (categoryId == 0 && !x.OwnerCategoryId.HasValue || x.Id == categoryId));

            query = query.SelectMany(x => x.OwnedCategories).Union(query);

            var categoryIds = await query.Select(x => x.Id).Distinct().ToArrayAsync();

            return await DbContext.Product.Where(x => categoryIds.Contains(x.CategoryId)
                                                && x.IsDeleted == false && x.IsHidden == false &&
                                                x.IsInTrash == false && x.Category.IsDeleted == false && x.Category.Disabled == false)
                .Include(x => x.Category).Include(x => x.ProductFiles)
                .Include("ProductFiles.File").ToListAsync();
            
        }
    }
}