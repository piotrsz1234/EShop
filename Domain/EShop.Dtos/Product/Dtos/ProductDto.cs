namespace EShop.Dtos.Product.Dtos;

public sealed class ProductDto
{
    public long Id { get; set; }
    public long? NewerVersionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public bool IsInTrash { get; set; }
    public bool IsHidden { get; set; }
    public int VatValue { get; set; }
    public string CategoryName { get; set; }
}