using SirketYonetim.Models.Common;

namespace SirketYonetim.Models.Product
{
    public class ProductViewModel : BaseViewModel
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductStock { get; set; }
    }
}
