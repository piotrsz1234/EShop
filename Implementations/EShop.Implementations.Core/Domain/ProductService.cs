using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Product;
using EShop.Dtos.Product.Models;
using EShop.Implementations.Core.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model, long bigImageId, long smallImageId, IReadOnlyCollection<long> otherFiles)
        {
            try {
                var item = model.Id.HasValue ? await _productRepository.GetOneAsync(model.Id.Value) : null;

                var product = MappingHelper.Mapper.Map<Product>(model);

                if (item != null) {
                    _productRepository.Remove(item);
                }

                product.ProductFiles.Add(new ProductFile() {
                    FileId = bigImageId
                });

                product.ProductFiles.Add(new ProductFile() {
                    FileId = smallImageId
                });

                foreach (var fileId in otherFiles) {
                    product.ProductFiles.Add(new ProductFile() {
                        FileId = fileId
                    });
                }

                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                return new AddEditProductResult() {
                    Id = product.Id
                };
            } catch (Exception) {
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
    }
}