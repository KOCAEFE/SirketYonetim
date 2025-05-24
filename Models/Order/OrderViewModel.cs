using SirketYonetim.Models.Common;

namespace SirketYonetim.Models.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public string OrderName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string CustomerName { get; set; }

        public Guid CustomerId { get; set; }
    }
}
