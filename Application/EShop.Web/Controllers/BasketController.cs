using EShop.Core.Domain;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        
        
    }
}