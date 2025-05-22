using Microsoft.EntityFrameworkCore;
using SirketYonetim.Entities;
using SirketYonetim.Models.Customer;
using SirketYonetim.Repositories.Abstract.Customer;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        protected readonly ICustomerReadRepository _customerReadRepository;
        protected readonly ICustomerWriteRepository _customerWriteRepository;

        public CustomerService(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            var customers = await _customerReadRepository.GetAll().ToListAsync();

            return customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).ToList();
        }

        public async Task<CustomerViewModel> GetByIdAsync(Guid id)
        {
            var customer = await _customerReadRepository.GetByIdAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            return new CustomerViewModel
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedDate = customer.CreatedDate,
                UpdatedDate = customer.UpdatedDate,
            };
        }

        public async Task AddAsync(CustomerCreateViewModel model)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedDate = DateTime.Now
            };

            await _customerWriteRepository.AddAsync(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerUpdateViewModel model)
        {
            var customer = await _customerReadRepository.GetByIdAsync(model.Id);
            if (customer == null)
                throw new Exception("Customer not found");

            customer.FullName = model.FullName;
            customer.Email = model.Email;
            customer.PhoneNumber = model.PhoneNumber;
            customer.UpdatedDate = DateTime.Now;

            await _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _customerReadRepository.GetByIdAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");

            _customerWriteRepository.Delete(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }
    }
}
