using EShop.Dtos.Order.Dtos;
using EShop.Dtos.Order.Models;

namespace EShop.Core.Domain;

public interface IOrderService
{
    Task<long?> CreateOrderAsync(CreateOrderModel model);
    Task ChangeStatusOfOrderAsync(ChangeOrderStatusModel model);
}