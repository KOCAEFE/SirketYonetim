using SirketYonetim.Entities.Common;

namespace SirketYonetim.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductStock { get; set; }

        // Bir ürün birden fazla siparişle ilişkili olabilir
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
