using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Customer;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> List()
        {
            var customers = await _customerService.GetAllAsync();
            return View(customers);
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return View(customer);
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return NotFound();

            var updateModel = new CustomerUpdateViewModel
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            return View(updateModel);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _customerService.UpdateAsync(model);
            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
