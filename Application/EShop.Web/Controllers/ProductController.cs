using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Product.Models;
using EShop.Web.Models;
using System.Linq;
using EShop.Implementations.Core.Domain;
using EShop.Dtos.Order.Dtos;
using System.Diagnostics;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductService productService, IFileService fileService, ICategoryRepository categoryRepository)
        {
            _productService = productService;
            _fileService = fileService;
            _categoryRepository = categoryRepository;
        }

        public async Task<ActionResult> AddEdit(long? productId = null)
        {
            var product = productId is null ? null : await _productService.GetProductAsync(productId.Value);
            List<Category> categories = (await _categoryRepository.GetAllAsync()).ToList();
            return View(System.Tuple.Create(product, categories));
        }

        [HttpPost]
        public async Task<ActionResult> AddEdit(AddEditProductViewModel model)
        {
            var smallImage = Request.Files["SmallImage"];
            var bigImage = Request.Files["BigImage"];
            var coreModel = new AddEditProductModel()
            {
                Files = new List<long>(),
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = decimal.Parse((model.Price).Replace(".", ",")),
                Id = model.OldVersionProductId
            };
            Debug.WriteLine("OldVersion: " + model.OldVersionProductId.ToString());

            if (smallImage.ContentLength > 0)
            {
                coreModel.Files.Add(await _fileService.UploadFileAsync(await smallImage.InputStream.ToArrayAsync(),
                    smallImage.FileName,
                    string.Empty, FileType.SmallImage));
            }

            if (bigImage.ContentLength > 0)
            {
                if (smallImage.ContentLength == 0)
                {
                    var files = await _fileService.UploadBigImageAndResizeAsync(
                        await bigImage.InputStream.ToArrayAsync(),
                        bigImage.FileName, string.Empty);
                    coreModel.Files.Add(files.Item1);
                    coreModel.Files.Add(files.Item2);
                }
                else
                {
                    coreModel.Files.Add(await _fileService.UploadFileAsync(await bigImage.InputStream.ToArrayAsync(),
                        bigImage.FileName, string.Empty, FileType.SmallImage));
                }
            }

            int index = 0;
            if (model.FileNames != null)
            {
                foreach (var fileName in model.FileNames)
                {
                    var file = Request.Files[$"File_{index}"];

                    if (file != null && file.ContentLength > 0)
                    {
                        coreModel.Files.Add(await _fileService.UploadFileAsync(await file.InputStream.ToArrayAsync(),
                            file.FileName, fileName, FileType.SmallImage));
                    }

                    index++;
                }
            }

            var result = await _productService.AddEditProductAsync(coreModel);

            if (result.Id > 0)
            {
                return RedirectToAction("Details", "Product", new { productId = result.Id });
            }

            return View();
        }

        public ActionResult AddEditAddress()
        {
            return View("_AddressAddEdit");
        }

        public async Task<ActionResult> ProductList()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<ActionResult> ProductListByCategory(long categoryId)
        {
            var products = await _productService.GetAllFromCategoryAsync(categoryId);
            return View("ProductList", products);
        }
        
        public ActionResult Details(long productid)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Remove(long? productId = null)
        {
            var product = productId is null ? null : await _productService.GetProductAsync(productId.Value);
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(long productId)
        {
            try
            {
                await _productService.RemoveProductAsync(productId);
            }
            finally
            {

            }
            return RedirectToAction("ProductList");
        }
    }
}