using System.ComponentModel.DataAnnotations;

namespace SirketYonetim.Models.Product
{
    public class ProductCreateViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal ProductPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int ProductStock { get; set; }
    }
}
