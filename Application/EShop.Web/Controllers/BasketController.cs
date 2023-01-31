using EShop.Core.Domain;
using EShop.Web.Helpers;
using EShop.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    [Authorize]
    [EnforceUser]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public BasketController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<ActionResult> Index()
        {
            var items = await _basketService.GetBasketItemsAsync(SessionHelper.LoggedUser.Id);

            return View(items);
        }

        [HttpPost]
        public async Task<ActionResult> AddOneMore(long productId)
        {
            await _basketService.AddProductToBasketAsync(SessionHelper.LoggedUser.Id, productId, 1);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> RemoveOne(long productId)
        {
            await _basketService.RemoveItemFromBasketAsync(SessionHelper.LoggedUser.Id, productId, true);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> RemoveAll(long productId)
        {
            await _basketService.RemoveItemFromBasketAsync(SessionHelper.LoggedUser.Id, productId, false);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderModel model)
        {
            var coreModel = new Dtos.Order.Models.CreateOrderModel() {
                AddressId = model.AddressId,
                PaymentType = model.PaymentType,
                ShippingMethodId = model.ShippingMethodId,
                UserId = SessionHelper.LoggedUser.Id
            };

            var result = await _orderService.CreateOrderAsync(coreModel);

            if (result.HasValue) {
                return RedirectToAction("ViewOrder", "Order", new { orderId = result.Value });
            }

            return View();
        }
    }
}