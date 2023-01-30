using EShop.Core.Domain;
using System.Threading.Tasks;
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

        public async Task<ActionResult> ViewOrder(long orderId)
        {
            var order = await _orderService.GetOrderDetails(orderId);
            return View(order);
        }
    }
}