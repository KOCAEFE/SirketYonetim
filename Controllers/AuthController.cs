using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Auth;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterAsync(model, "Customer");

            if (result.Succeeded)
                return RedirectToAction("LoginCustomer");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // LOGIN
        [HttpGet]
        public IActionResult LoginCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginCustomer(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model, "Customer");

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", result.IsLockedOut
                ? "Your account has been locked. Please try again later."
                : "Invalid login or you do not have permission to access this area.");

            return View(model);
        }

        [HttpGet]
        public IActionResult LoginEmployee() => View();

        [HttpPost]
        public async Task<IActionResult> LoginEmployee(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model, "Employee", "Admin");

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", result.IsLockedOut
                ? "Your account has been locked. Please try again later."
                : "Invalid login or you do not have permission to access this area.");

            return View(model);
        }

        // LOGOUT
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index");
        }
    }
}
