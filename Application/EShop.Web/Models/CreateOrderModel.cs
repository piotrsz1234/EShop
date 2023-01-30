using EShop.Core.Common.Enums;

namespace EShop.Web.Models
{
    public class CreateOrderModel
    {
        public long AddressId { get; set; }
        public PaymentType PaymentType { get; set; }
        public long ShippingMethodId { get; set; }
    }
}