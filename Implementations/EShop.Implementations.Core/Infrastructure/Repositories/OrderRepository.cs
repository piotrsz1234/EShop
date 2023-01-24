using System.Data.Entity;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories;

internal class OrderRepository : RepositoryGenericBase<Order>, IOrderRepository
{
    public OrderRepository(MainDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> GetLastOrderNumberAsync()
    {
        var result = await DbContext.Order.OrderByDescending(x => x.InsertDateUtc)
            .FirstOrDefaultAsync(x => x.IsDeleted == false);

        return result?.OrderNumber;
    }
}