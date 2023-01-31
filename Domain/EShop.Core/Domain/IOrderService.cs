using EShop.Dtos.Order.Dtos;
using EShop.Dtos.Order.Models;

namespace EShop.Core.Domain;

public interface IOrderService
{
    Task<long?> CreateOrderAsync(CreateOrderModel model);
    Task ChangeStatusOfOrderAsync(ChangeOrderStatusModel model);
    Task<OrderDto> GetOrderDetails(long orderId);
    Task<IReadOnlyCollection<OrderDto>> GetUsersOrdersAsync(long userId);
    Task<IReadOnlyCollection<OrderDto>> GetUncompletedOrdersAsync();
    Task SetNextStatusAsync(long orderId);
    Task CancelOrderAsync(long orderId);
}