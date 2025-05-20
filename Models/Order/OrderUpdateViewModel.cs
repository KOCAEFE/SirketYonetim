using SirketYonetim.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SirketYonetim.Models.Order
{
    public class OrderUpdateViewModel : BaseViewModel
    {
        [Required]
        public string OrderName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
