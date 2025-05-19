using SirketYonetim.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SirketYonetim.Entities
{
    public class Order : BaseEntity
    {
        public string OrderName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        
        public ICollection<OrderProduct> OrderProducts { get; set; }


    }
}
