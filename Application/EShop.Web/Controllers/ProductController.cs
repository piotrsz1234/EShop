﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Extensions;
using EShop.Dtos.Product.Models;
using EShop.Web.Models;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;

        public ProductController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        public ActionResult AddEdit()
        {
            return View();
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
                CategoryId = 1,
                Description = model.Description,
                Price = 10
            };

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

        public async Task<ActionResult> ProductList(long categoryId = 0)
        {
            var products = await _productService.GetAllFromCategoryAsync(categoryId);

            return View(products);
        }
    }
}