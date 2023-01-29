namespace EShop.Dtos.Order.Dtos
{
    public class CategoryDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OwnerCategoryId { get; set; }
        public bool IsHidden { get; set; }
        public string HiddenText { get; set; }
        public string OwnerCategoryName { get; set; }
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
    }
}