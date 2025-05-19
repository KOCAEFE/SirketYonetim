using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Order;

namespace SirketYonetim.Repositories.Concrete.Order
{
    public class OrderReadRepository : ReadRepository<Entities.Order>, IOrderReadRepository
    {
        public OrderReadRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
