using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Order;

namespace SirketYonetim.Repositories.Concrete.Order
{
    public class OrderWriteRepository : WriteRepository<Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
