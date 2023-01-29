using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;

namespace EShop.Implementations.Core.Domain
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> GetCategoryAsync(long categoryId)
        {
            try {
                var category = await _categoryRepository.GetOneAsync(categoryId);

                return new CategoryDto() {
                    Id = category.Id,
                    Name = category.Name,
                    OwnerCategoryId = category.OwnerCategoryId,
                    IsHidden = category.Disabled,
                };
            } catch (Exception e) {
                return null;
            }
        }

        public async Task<bool> AddEditCategoryAsync(CategoryDto category)
        {
            try {
                if (category.Id.HasValue) {
                    var entity = await _categoryRepository.GetOneAsync(category.Id.Value);
                    entity.Disabled = category.HiddenText?.ToLower()?.Trim() == "on";
                    entity.OwnerCategoryId = category.OwnerCategoryId;
                    entity.Name = category.Name;
                    _categoryRepository.ForceUpdate(entity);
                } else {
                    await _categoryRepository.AddAsync(new Category() {
                        Disabled = category.HiddenText?.ToLower()?.Trim() == "on",
                        OwnerCategoryId = category.OwnerCategoryId,
                        Name = category.Name,
                    });
                }

                await _categoryRepository.SaveChangesAsync();

                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public async Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
        {
            try {
                var data = await _categoryRepository.GetAllAsync(x => x.IsDeleted == false, x => x.OwnerCategory);

                return data.Select(x => new CategoryDto() {
                    Id = x.Id,
                    Name = x.Name,
                    IsHidden = x.Disabled,
                    OwnerCategoryId = x.OwnerCategoryId,
                    OwnerCategoryName = x.OwnerCategory?.Name,
                    ProductCount = x.Products.Count,
                    CategoryCount = x.OwnedCategories.Count,
                }).ToList();
            } catch (Exception e) {
                return Array.Empty<CategoryDto>();
            }
        }

        public async Task RemoveCategoryAsync(long categoryId)
        {
            _categoryRepository.Remove(await _categoryRepository.GetOneAsync(categoryId));

            await _categoryRepository.SaveChangesAsync();
        }
    }
}