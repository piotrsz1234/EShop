using EShop.Core.Domain;
using EShop.Web.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    [Authorize]
    [EnforceUser]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<ActionResult> ViewOrder(long orderId)
        {
            var order = await _orderService.GetOrderDetails(orderId);
            return View(order);
        }
        
        public async Task<ActionResult> MyOrderList()
        {
            var orders = await _orderService.GetUsersOrdersAsync(SessionHelper.LoggedUser.Id);

            return View(orders);
        }
        
        public async Task<ActionResult> ManageOrders()
        {
            var orders = await _orderService.GetUncompletedOrdersAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<ActionResult> SetNextStatus(long orderId)
        {
            await _orderService.SetNextStatusAsync(orderId);

            return RedirectToAction("ManageOrders");
        }
        
        [HttpPost]
        public async Task<ActionResult> CancelOrder(long orderId)
        {
            await _orderService.CancelOrderAsync(orderId);

            return RedirectToAction("ManageOrders");
        }

        [HttpPost]
        public async Task<ActionResult> AddItemsFromOrder(long orderId)
        {
            await _basketService.AddItemsFromOrderAsync(orderId, SessionHelper.LoggedUser.Id);

            return RedirectToAction("Index", "Basket");
        }
    }
}