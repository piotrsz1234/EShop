using EShop.Dtos.Order.Dtos;
using EShop.Dtos.Product;
using EShop.Dtos.Product.Dtos;
using EShop.Dtos.Product.Models;
using EShop.Dtos.Product.Results;

namespace EShop.Core.Domain
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(long productId);
        Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model);
        //Task<bool> RemoveProductAsync(long id);
        Task RemoveProductAsync(long productId);
        Task<IReadOnlyCollection<ProductDto>> GetAllFromCategoryAsync(long categoryId, bool isAdmin);
        Task<IReadOnlyCollection<ProductDto>> GetAllProductsAsync(bool isAdmin);
    }
}