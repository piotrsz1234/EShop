using EShop.Dtos.Product;
using EShop.Dtos.Product.Dtos;
using EShop.Dtos.Product.Models;
using EShop.Dtos.Product.Results;

namespace EShop.Core.Domain
{
    public interface IProductService
    {
        Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model);
        Task<bool> RemoveProductAsync(long id);
        Task<IReadOnlyCollection<ProductDto>> GetAllFromCategoryAsync(long categoryId);
    }
}