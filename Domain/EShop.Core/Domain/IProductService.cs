using EShop.Dtos.Product;
using EShop.Dtos.Product.Models;
using EShop.Dtos.Product.Results;

namespace EShop.Core.Domain
{
    public interface IProductService
    {
        Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model, long bigImageId, long smallImageId, IReadOnlyCollection<long> otherFiles);
        Task<bool> RemoveProductAsync(long id);
    }
}