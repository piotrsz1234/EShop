namespace EShop.Dtos.Product.Models
{
    public class AddEditProductModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsHidden { get; set; }
        public int VatValue { get; set; }

        public ICollection<long> Files { get; set; }
    }
}