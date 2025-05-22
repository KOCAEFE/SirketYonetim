using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Order;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return View(order);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            var updateModel = new OrderUpdateViewModel
            {
                Id = order.Id,
                OrderName = order.OrderName,
                Description = order.Description,
                Address = order.Address,
                CustomerId = order.CustomerId
            };

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _orderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
