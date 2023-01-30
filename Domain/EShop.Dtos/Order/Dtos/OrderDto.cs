using EShop.Core.Common.Enums;
using EShop.Dtos.Product.Dtos;
using EShop.Dtos.User.Dtos;

namespace EShop.Dtos.Order.Dtos;

public sealed class OrderDto
{
    public long OrderId { get; set; }
    public string OrderNumber { get; set; }
    public AddressDto Address { get; set; }
    public IReadOnlyCollection<OrderItemDto> Items { get; set; }
    public ShippingMethodDto ShippingMethod { get; set; }
    public PaymentType PaymentType { get; set; }
    public OrderStatus OrderStatus { get; set; }
}