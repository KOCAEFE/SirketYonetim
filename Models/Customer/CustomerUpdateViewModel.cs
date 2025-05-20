using SirketYonetim.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SirketYonetim.Models.Customer
{
    public class CustomerUpdateViewModel : BaseViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}
