using SirketYonetim.Models.Common;

namespace SirketYonetim.Models.Customer
{
    public class CustomerViewModel : BaseViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AppUserId { get; set; }
    }
}
