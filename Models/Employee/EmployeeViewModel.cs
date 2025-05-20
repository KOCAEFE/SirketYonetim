using SirketYonetim.Models.Common;

namespace SirketYonetim.Models.Employee
{
    public class EmployeeViewModel : BaseViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AppUserId { get; set; }
    }
}
