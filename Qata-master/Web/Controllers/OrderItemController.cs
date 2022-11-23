using Business.OrderItemManagement;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class OrderItemController : Controller
    {
        private IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        public IActionResult Index()
        {
            var result = _orderItemService.GetAll();
            if (result.Success)
            {
                return View(result.Data);
            }
            return View();
        }
        [HttpGet]
        public IActionResult NewOrderItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewOrderItem(OrderItem orderItem)
        {
            var result = _orderItemService.Add(orderItem);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return View(orderItem);
        }

        [HttpGet]
        public IActionResult UpdateOrderItem(int id)
        {
            var result = _orderItemService.GetOrderItemById(id);
            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateOrderItem(OrderItem orderItem)
        {
            var result = _orderItemService.Update(orderItem);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteOrderItem(int id)
        {
            var orderItem = _orderItemService.GetOrderItemById(id).Data;
            var result = _orderItemService.Delete(orderItem);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
