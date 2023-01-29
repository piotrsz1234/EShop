using EShop.Core.Domain;
using EShop.Dtos.Order.Dtos;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> AddEdit(long? categoryId = null)
        {
            var category = categoryId is null ? null : await _categoryService.GetCategoryAsync(categoryId.Value);
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddEdit(CategoryDto category)
        {
            if (await _categoryService.AddEditCategoryAsync(category)) {
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        public async Task<ActionResult> CategoryList()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return View(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(long categoryId)
        {
            try {
                await _categoryService.RemoveCategoryAsync(categoryId);
            } finally {
                
            }
            return RedirectToAction("CategoryList");
        }
    }
}