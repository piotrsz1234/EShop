using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Product;
using EShop.Dtos.Product.Dtos;
using EShop.Dtos.Product.Models;
using EShop.Dtos.Product.Results;
using EShop.Implementations.Core.Utils;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model)
        {
            try {
                var item = model.Id.HasValue ? await _productRepository.GetOneAsync(model.Id.Value) : null;

                var product = MappingHelper.Mapper.Map<Product>(model);

                product.InsertDateUtc = DateTime.UtcNow;
                product.ModificationDateUtc = DateTime.UtcNow;

                if (item != null) {
                    _productRepository.Remove(item);
                }

                foreach (var fileId in model.Files) {
                    product.ProductFiles.Add(new ProductFile() {
                        FileId = fileId
                    });
                }

                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                return new AddEditProductResult() {
                    Id = product.Id
                };
            } catch (Exception e) {
                return null;
            }
        }

        public async Task<bool> RemoveProductAsync(long id)
        {
            try {
                var item = await _productRepository.GetOneAsync(id);

                if (item is null) return false;
                
                _productRepository.Remove(item);
                
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<IReadOnlyCollection<ProductDto>> GetAllFromCategoryAsync(long categoryId)
        {
            try
            {
                var data = await _productRepository.GetAllFromCategoryAsync(categoryId);

                return data.Select(x => new ProductDto()
                {
                    CategoryId = x.CategoryId,
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    CategoryName = x.Category.Name,
                    VatValue = x.VatValue,
                    BigImageId = x.ProductFiles.FirstOrDefault(x => x.File.Type == FileType.MainImage)?.Id,
                    SmallImageId = x.ProductFiles.FirstOrDefault(x => x.File.Type == FileType.SmallImage)?.Id
                }).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}