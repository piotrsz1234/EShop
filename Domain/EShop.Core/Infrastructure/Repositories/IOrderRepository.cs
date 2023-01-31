using EShop.Core.Entities;

namespace EShop.Core.Infrastructure.Repositories;

public interface IOrderRepository : IRepositoryGenericBase<Order>
{
    Task<string> GetLastOrderNumberAsync();
    void Reload(Order order);
    Task<Order> GetForMail(long id);
}