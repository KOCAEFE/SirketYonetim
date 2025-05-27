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
        private readonly CurrencyService _currencyService;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _currencyService = new CurrencyService();
        }
        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var products = await _productReadRepository.GetAll().ToListAsync();

            var currencies = new[] { "USD", "EUR", "GBP", "AUD", "NOK", "RUB", "AZN", "CHF", "CNY", "JPY" };

            var rates = await _currencyService.GetExchangeRatesAsync("TRY", currencies);

            if (rates == null)
            {
                Console.WriteLine("Kur bilgisi alınamadı.");
            }
            else
            {
                foreach (var c in currencies)
                {
                    if (rates.TryGetValue(c, out var r))
                        Console.WriteLine($"Kur TRY -> {c} : {r}");
                    else
                        Console.WriteLine($"Kur bulunamadı: {c}");
                }
            }

            return products.Select(p =>
            {
                var exchangePrices = new Dictionary<string, decimal>();

                if (rates != null && rates.Count > 0)
                {
                    foreach (var currency in currencies)
                    {
                        if (rates.TryGetValue(currency, out decimal rate) && rate > 0)
                        {
                            var convertedPrice = Math.Round(p.ProductPrice * rate, 2);
                            Console.WriteLine($"Ürün: {p.ProductName} TRY fiyatı: {p.ProductPrice} - {currency} fiyatı: {convertedPrice}");
                            exchangePrices[currency] = convertedPrice;
                        }
                    }
                }

                return new ProductViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductStock = p.ProductStock,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    ExchangePrices = exchangePrices
                };
            }).ToList();
        }


        //public async Task<List<ProductViewModel>> GetAllAsync()
        //{
        //    var products = await _productReadRepository.GetAll().ToListAsync();

        //    // Döviz kurları için örneklem
        //    var currencies = new[] { "USD", "EUR", "GBP", "AUD", "NOK", "DKK", "AZN", "CHF", "CNY", "JPY" };

        //    var rates = await _currencyService.GetExchangeRatesAsync("TRY", currencies); // Kur bilgileri getirilecek

        //    return products.Select(p =>
        //    {
        //        var exchangePrices = new Dictionary<string, decimal>();

        //        if (rates != null && rates.Count > 0)
        //        {
        //            foreach (var currency in currencies)
        //            {
        //                if (rates.TryGetValue(currency, out decimal rate) && rate > 0)
        //                {
        //                    // TRY / rate = o dövizdeki fiyat
        //                    exchangePrices[currency] = Math.Round(p.ProductPrice * rate, 2);
        //                }
        //            }
        //        }

        //        return new ProductViewModel
        //        {
        //            Id = p.Id,
        //            ProductName = p.ProductName,
        //            ProductPrice = p.ProductPrice,
        //            ProductStock = p.ProductStock,
        //            CreatedDate = p.CreatedDate,
        //            UpdatedDate = p.UpdatedDate,
        //            ExchangePrices = exchangePrices
        //        };
        //    }).ToList();
        //}

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
