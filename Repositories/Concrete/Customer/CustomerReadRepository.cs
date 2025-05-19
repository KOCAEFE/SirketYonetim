using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Customer;

namespace SirketYonetim.Repositories.Concrete.Customer
{
    public class CustomerReadRepository : ReadRepository<Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
