using SirketYonetim.Models.Customer;

namespace SirketYonetim.Services.Abstract
{
    public interface ICustomerService
    {
        Task<List<CustomerViewModel>> GetAllAsync();

        Task<CustomerViewModel> GetByIdAsync(Guid id);

        Task AddAsync(CustomerCreateViewModel model);

        Task UpdateAsync(CustomerUpdateViewModel model);

        Task DeleteAsync(Guid id);
    }
}
