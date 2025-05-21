using Microsoft.AspNetCore.Identity;
using SirketYonetim.Models.Identity;

namespace SirketYonetim.Services.Abstract
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> AssignRoleAsync(string userId, string roleName);
    }

}
