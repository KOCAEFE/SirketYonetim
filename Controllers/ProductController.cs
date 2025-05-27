using Microsoft.AspNetCore.Mvc;
using SirketYonetim.Models.Product;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
    }
}
