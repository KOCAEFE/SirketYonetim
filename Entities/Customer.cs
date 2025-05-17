using SirketYonetim.Entities.Common;

namespace SirketYonetim.Entities
{
    public class Customer:BaseEntity
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
