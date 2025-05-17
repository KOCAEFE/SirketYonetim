using System.ComponentModel.DataAnnotations.Schema;

namespace SirketYonetim.Entities
{
    public class Order
    {
        public string OrderName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        
        public ICollection<Product> Products { get; set; }


    }
}
