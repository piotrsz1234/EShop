using EShop.Core.Common.Enums;

namespace EShop.Dtos.Order.Models;

public sealed class ChangeOrderStatusModel
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
}