﻿using EShop.Core.Entities;

namespace EShop.Core.Infrastructure.Repositories
{
    public interface IProductRepository : IRepositoryGenericBase<Product>
    {
        Task<List<Product>> GetAllFromCategoryAsync(long categoryId);
    }
}