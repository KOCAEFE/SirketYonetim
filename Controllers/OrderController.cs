using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Order;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> List()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return View(order);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateAsync(model);
                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _orderService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}