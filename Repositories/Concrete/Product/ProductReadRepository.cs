using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Product;

namespace SirketYonetim.Repositories.Concrete.Product
{
    public class ProductReadRepository : ReadRepository<Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
