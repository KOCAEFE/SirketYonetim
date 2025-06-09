using Microsoft.AspNetCore.Identity;
using SirketYonetim.Models.Auth;

namespace SirketYonetim.Services.Abstract
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model, params string[] allowedRoles);

        Task LogoutAsync();

        Task<IdentityResult> AssignRoleAsync(string userId, string roleName);
    }

}
