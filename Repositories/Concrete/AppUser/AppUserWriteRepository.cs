using Microsoft.AspNetCore.Identity;
using SirketYonetim.Repositories.Abstract.AppUser;

namespace SirketYonetim.Repositories.Concrete.AppUser
{
    public class AppUserWriteRepository : IAppUserWriteRepository
    {
        private readonly UserManager<Entities.AppUser> _userManager;

        public AppUserWriteRepository(UserManager<Entities.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(Entities.AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;

            // Varsayılan olarak "Customer" rolü ata
            await _userManager.AddToRoleAsync(user, "Customer");
            return true;
        }

        public async Task<bool> Update(Entities.AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }
    }
}
