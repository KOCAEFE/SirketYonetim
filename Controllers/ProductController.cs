using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Order;
using SirketYonetim.Models.Product;
using SirketYonetim.Repositories.Abstract.Customer;
using SirketYonetim.Services.Abstract;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SirketYonetim.Services.Concrete;

namespace SirketYonetim.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICustomerReadRepository _customerReadRepository;

        public ProductController(IProductService productService, ICustomerReadRepository customerReadRepository, IOrderService orderService)
        {
            _productService = productService;
            _customerReadRepository = customerReadRepository;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }


        public async Task<IActionResult> List()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Buy(Guid productId, string address)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var customer = await _customerReadRepository
                .GetAll()
                .Where(c => c.AppUserId == userId)
                .FirstOrDefaultAsync();

            if (customer == null)
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(address))
                ModelState.AddModelError("Address", "Adres zorunludur.");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("List");
            }

            var model = new OrderCreateViewModel
            {
                OrderName = "Purchasing Products",
                Description = "Auto-generated.",
                Address = address,
                CustomerId = customer.Id,
                ProductId = productId
            };

            var newOrderId = await _orderService.AddAsync(model);

            //await _orderService.AddAsync(model);
            return RedirectToAction("Detail", "Order", new { id = newOrderId });
        }


    }
}