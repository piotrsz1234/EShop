using EShop.Dtos.Product;
using EShop.Dtos.Product.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Domain
{
    public interface IProductService
    {
        Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model, long bigImageId, long smallImageId, IReadOnlyCollection<long> otherFiles);
        Task<bool> RemoveProductAsync(long id);
    }
}