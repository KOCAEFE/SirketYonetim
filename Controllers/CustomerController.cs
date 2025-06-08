using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Customer;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> List()
        {
            var customers = await _customerService.GetAllAsync();
            return View(customers);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _customerService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _customerService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}