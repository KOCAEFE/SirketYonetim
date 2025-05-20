using System.ComponentModel.DataAnnotations;

namespace SirketYonetim.Models.Common
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
