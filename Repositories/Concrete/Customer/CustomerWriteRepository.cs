using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Customer;

namespace SirketYonetim.Repositories.Concrete.Customer
{
    public class CustomerWriteRepository : WriteRepository<Entities.Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
