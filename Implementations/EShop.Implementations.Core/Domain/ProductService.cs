using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
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
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<ProductDto> GetProductAsync(long productId)
        {
            try {
                var product = await _productRepository.GetOneAsync(productId);

                return new ProductDto() {
                    CategoryId = product.CategoryId,
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name,
                    VatValue = product.VatValue,
                    BigImageId = product.ProductFiles.FirstOrDefault(x => x.File.Type == FileType.MainImage)?.Id,
                    SmallImageId = product.ProductFiles.FirstOrDefault(x => x.File.Type == FileType.SmallImage)?.Id
                };
            } catch (Exception e) {
                return null;
            }
        }

        public async Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model)
        {
            try {
                var item = model.Id.HasValue ? await _productRepository.GetOneAsync(model.Id.Value) : null;
                
                var product = MappingHelper.Mapper.Map<Product>(model);
                product.InsertDateUtc = DateTime.UtcNow;
                product.ModificationDateUtc = DateTime.UtcNow;
                product.OldVersionProductId = item?.Id;
                
                if (item != null) {
                    _productRepository.Remove(item);
                }

                foreach (var fileId in model.Files) {
                    product.ProductFiles.Add(new ProductFile() {
                        FileId = fileId
                    });
                }

                if (item != null) {
                    foreach (var file in item.ProductFiles) {
                        if (file.File.Type == FileType.MainImage && product.ProductFiles.Any(x => x.IsDeleted == false && x.File?.Type == FileType.MainImage))
                            continue;

                        if (file.File.Type == FileType.SmallImage && product.ProductFiles.Any(x => x.IsDeleted == false && x.File?.Type == FileType.SmallImage))
                            continue;

                        product.ProductFiles.Add(new ProductFile() {
                            FileId = file.FileId
                        });
                    }
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
            try {
                var categories = await _categoryRepository.GetAllWithDependentAsync(categoryId);

                var data = await _productRepository.GetAllAsync(x => x.IsHidden == false
                                                                     && x.IsDeleted == false
                                                                     && x.IsInTrash == false
                                                                     && categories.Contains(x.CategoryId));

                return data.Select(x => new ProductDto() {
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
            } catch {
                return null;
            }
        }

        public async Task<IReadOnlyCollection<ProductDto>> GetAllProductsAsync()
        {
            try {
                var data = await _productRepository.GetAllAsync(x => x.IsHidden == false
                                                                     && x.IsDeleted == false
                                                                     && x.IsInTrash == false);

                return data.Select(x => new ProductDto() {
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
            } catch {
                return null;
            }
        }
    }
}