using Microsoft.EntityFrameworkCore;
using SirketYonetim.Entities;
using SirketYonetim.Models.Order;
using SirketYonetim.Repositories.Abstract.Order;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class OrderService : IOrderService
    {
        protected readonly IOrderReadRepository _orderReadRepository;
        protected readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<List<OrderViewModel>> GetAllAsync()
        {
            var orders = await _orderReadRepository.GetAll().Include(c => c.Customer).ToListAsync();

            return orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                OrderName = o.OrderName,
                Description = o.Description,
                Address = o.Address,
                CustomerName = o.Customer.FullName,
                CustomerId = o.CustomerId,
                CreatedDate = o.CreatedDate,
                UpdatedDate = o.UpdatedDate
            }).ToList();
        }

        public async Task<OrderDetailViewModel> GetByIdAsync(Guid id)
        {
            var order = await _orderReadRepository.GetAll()
        .Include(o => o.Customer)
        .Include(o => o.OrderProducts) // Eğer ürünler varsa, ilişkili tabloya göre düzenle
        .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new Exception("Order not found");

            // Nominatim ile koordinat alma
            var nominatim = new NominatimService();
            var (lat, lon) = await nominatim.GetCoordinatesAsync(order.Address);

            return new OrderDetailViewModel
            {
                Id = order.Id,
                OrderName = order.OrderName,
                Description = order.Description,
                Address = order.Address,
                CustomerName = order.Customer.FullName,
                CustomerId = order.CustomerId,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                ProductNames = order.OrderProducts?.Select(p => p.Product.ProductName).ToList() ?? new List<string>(),
                Latitude = lat,
                Longitude = lon
            };
        }

        public async Task AddAsync(OrderCreateViewModel model)
        {
            var nominatim = new NominatimService();
            var (lat, lon) = await nominatim.GetCoordinatesAsync(model.Address);

            if (lat == null || lon == null)
                throw new Exception("Address verification failed. Please enter a valid address.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderName = model.OrderName,
                Description = model.Description,
                Address = model.Address,
                CustomerId = model.CustomerId,
                CreatedDate = DateTime.Now
            };

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderUpdateViewModel model)
        {
            var order = await _orderReadRepository.GetByIdAsync(model.Id);
            if (order == null)
                throw new Exception("Order not found");

            order.OrderName = model.OrderName;
            order.Description = model.Description;
            order.Address = model.Address;
            order.CustomerId = model.CustomerId;
            order.UpdatedDate = DateTime.Now;

            await _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderReadRepository.GetByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            _orderWriteRepository.Delete(order);
            await _orderWriteRepository.SaveChangesAsync();
        }
    }
}
