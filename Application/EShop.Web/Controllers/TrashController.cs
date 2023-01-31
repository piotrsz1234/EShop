using EShop.Core.Domain;
using EShop.Web.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    [EnforceUser]
    [Authorize]
    public class TrashController : Controller
    {
        private readonly IProductService _productService;

        public TrashController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> Index()
        {
            var items = await _productService.GetAllFromTrashAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long productId)
        {
            await _productService.DeleteProductAsync(productId);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> RemoveProductFromTrash(long productId)
        {
            await _productService.RemoveProductFromTrashAsync(productId);

            return RedirectToAction("Index");
        }
    }
}