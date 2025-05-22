using Microsoft.EntityFrameworkCore;
using SirketYonetim.Entities;
using SirketYonetim.Models.Product;
using SirketYonetim.Repositories.Abstract.Product;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class ProductService : IProductService
    {
        protected readonly IProductReadRepository _productReadRepository;
        protected readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var products = await _productReadRepository.GetAll().ToListAsync();

            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductStock = p.ProductStock,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            }).ToList();
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            return new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate
            };
        }

        public async Task AddAsync(ProductCreateViewModel model)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductStock = model.ProductStock,
                CreatedDate = DateTime.Now
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductUpdateViewModel model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);
            if (product == null)
                throw new Exception("Product not found");

            product.ProductName = model.ProductName;
            product.ProductPrice = model.ProductPrice;
            product.ProductStock = model.ProductStock;
            product.UpdatedDate = DateTime.Now;

            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            _productWriteRepository.Delete(product);
            await _productWriteRepository.SaveChangesAsync();
        }
    }
}
