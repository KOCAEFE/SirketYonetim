using Microsoft.AspNetCore.Identity;
using SirketYonetim.Entities;
using SirketYonetim.Models.Identity;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync("Customer"); // Register başarılı ise Customer rolü var mı kontrol et
                if (!roleExists) // eğer yoksa oluştur
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                await _userManager.AddToRoleAsync(user, "Customer"); // Customer rolünü ata
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user.UserName,
                                                            model.Password,
                                                            model.RememberMe,
                                                            lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(roleName); // Rol yoksa oluştur
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
