using SirketYonetim.Models.Common;

namespace SirketYonetim.Models.Order
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public string OrderName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string CustomerName { get; set; }

        public List<string> ProductNames { get; set; }
    }
}
