using Microsoft.AspNetCore.Identity;
using SirketYonetim.Entities;
using SirketYonetim.Models.Auth;
using SirketYonetim.Models.Customer;
using SirketYonetim.Services.Abstract;

namespace SirketYonetim.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ICustomerService _customerService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _customerService = customerService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model, string roleName)
        {
            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.UserName.ToLower(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ImageUrl = "default.jpg"
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return result;

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new AppRole { Name = roleName });

            await _userManager.AddToRoleAsync(user, roleName);

            if (roleName.ToLower() == "customer")
            {
                var customerModel = new CustomerCreateViewModel
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    CreatedDate = DateTime.Now
                };

                await _customerService.AddAsync(customerModel);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model, params string[] allowedRoles)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return SignInResult.Failed;

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (!result.Succeeded)
                return result;

            var userRoles = await _userManager.GetRolesAsync(user);
            var hasAccess = allowedRoles.Any(role => userRoles.Contains(role));

            if (!hasAccess)
            {
                await _signInManager.SignOutAsync();
                return SignInResult.Failed;
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("No user found with the provided ID.");

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new AppRole { Name = roleName });

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
