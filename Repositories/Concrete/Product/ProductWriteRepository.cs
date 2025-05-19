using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.Product;

namespace SirketYonetim.Repositories.Concrete.Product
{
    public class ProductWriteRepository : WriteRepository<Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(SirketYonetimContext context) : base(context)
        {
        }
    }
}
