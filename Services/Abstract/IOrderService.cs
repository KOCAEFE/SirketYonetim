using SirketYonetim.Models.Order;

namespace SirketYonetim.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetAllAsync();

        Task<OrderViewModel> GetByIdAsync(Guid id);

        Task AddAsync(OrderCreateViewModel model);

        Task UpdateAsync(OrderUpdateViewModel model);

        Task DeleteAsync(Guid id);
    }
}
