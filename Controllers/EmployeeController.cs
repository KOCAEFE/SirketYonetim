using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Employee;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> List()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _employeeService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            var updateModel = new EmployeeUpdateViewModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _employeeService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}