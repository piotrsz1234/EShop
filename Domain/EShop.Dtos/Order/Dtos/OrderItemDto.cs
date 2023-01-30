using EShop.Dtos.Product.Dtos;

namespace EShop.Dtos.Order.Dtos
{
    public class OrderItemDto
    {
        public int Count { get; set; }
        public ProductDto Product { get; set; }
    }
}