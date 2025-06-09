using SirketYonetim.Models.Order;

namespace SirketYonetim.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetAllAsync();

        Task<OrderDetailViewModel> GetByIdAsync(Guid id);

        Task<Guid> AddAsync(OrderCreateViewModel model);

        Task UpdateAsync(OrderUpdateViewModel model);

        Task DeleteAsync(Guid id);
    }
}
