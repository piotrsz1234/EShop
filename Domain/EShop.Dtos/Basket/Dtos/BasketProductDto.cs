namespace EShop.Dtos.Basket.Dtos
{
    public class BasketProductDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long? FileId { get; set; }
        public int Count { get; set; }
    }
}