using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult AddEdit()
        {
            return View();
        }
        
        public ActionResult AddEditAddress()
        {
            return View("_AddressAddEdit");
        }
    }
}