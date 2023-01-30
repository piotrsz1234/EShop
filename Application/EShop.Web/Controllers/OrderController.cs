using EShop.Core.Domain;
using System.Web.Mvc;

namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult ViewOrder(long orderId)
        {
            var order = _orderService.GetOrderDetails(orderId);
            return View(order);
        }
    }
}