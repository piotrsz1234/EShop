using EShop.Dtos.Order.Dtos;

namespace EShop.Core.Domain
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryAsync(long categoryId);
        Task<bool> AddEditCategoryAsync(CategoryDto category);
        Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync();
        Task RemoveCategoryAsync(long categoryId);
    }
}