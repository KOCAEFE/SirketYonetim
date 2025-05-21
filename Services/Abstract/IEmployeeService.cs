using SirketYonetim.Models.Employee;

namespace SirketYonetim.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetAllAsync();

        Task<EmployeeViewModel> GetByIdAsync(Guid id);

        Task AddAsync(EmployeeCreateViewModel model);

        Task UpdateAsync(EmployeeUpdateViewModel model);

        Task DeleteAsync(Guid id);
    }
}
