using SirketYonetim.Entities.Common;

namespace SirketYonetim.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProducStock { get; set; }
    }
}
