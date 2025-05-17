using Microsoft.AspNetCore.Identity;

namespace SirketYonetim.Entity
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
