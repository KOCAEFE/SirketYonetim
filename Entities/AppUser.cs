using Microsoft.AspNetCore.Identity;

namespace SirketYonetim.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
