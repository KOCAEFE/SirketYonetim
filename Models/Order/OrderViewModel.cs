using SirketYonetim.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SirketYonetim.Models.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public string OrderName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string CustomerName { get; set; }
    }
}
