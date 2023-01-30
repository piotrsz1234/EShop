using EShop.Core.Common.Enums;

namespace EShop.Dtos.Order.Models;

public sealed class CreateOrderModel
{
    public long UserId { get; set; }
    public PaymentType PaymentType { get; set; }
    public long AddressId { get; set; }
    public long ShippingMethodId { get; set; }
}