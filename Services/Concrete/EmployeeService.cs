using Microsoft.EntityFrameworkCore;
using SirketYonetim.Entities;
using SirketYonetim.Models.Employee;
using SirketYonetim.Repositories.Abstract.Employee;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IEmployeeReadRepository _employeeReadRepository;
        protected readonly IEmployeeWriteRepository _employeeWriteRepository;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository, IEmployeeWriteRepository employeeWriteRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
        }

        public async Task<List<EmployeeViewModel>> GetAllAsync()
        {
            var employees = await _employeeReadRepository.GetAll().Include(e => e.AppUser).ToListAsync();

            return employees.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FullName = e.FullName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                AppUserId = e.AppUserId,
                CreatedDate = e.CreatedDate,
                UpdatedDate = e.UpdatedDate
            }).ToList();
        }

        public async Task<EmployeeViewModel> GetByIdAsync(Guid id)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found");

            return new EmployeeViewModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                AppUserId = employee.AppUserId,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
            };
        }

        public async Task AddAsync(EmployeeCreateViewModel model)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                AppUserId = model.AppUserId,
                CreatedDate = DateTime.Now
            };

            await _employeeWriteRepository.AddAsync(employee);
            await _employeeWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeUpdateViewModel model)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(model.Id);
            if (employee == null)
                throw new Exception("Employee not found");

            employee.FullName = model.FullName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.AppUserId = model.AppUserId;
            employee.UpdatedDate = DateTime.Now;

            await _employeeWriteRepository.Update(employee);
            await _employeeWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found");

            _employeeWriteRepository.Delete(employee);
            await _employeeWriteRepository.SaveChangesAsync();
        }
    }
}
