using Microsoft.EntityFrameworkCore;
using SirketYonetim.Data;
using SirketYonetim.Repositories.Abstract.AppUser;

namespace SirketYonetim.Repositories.Concrete.AppUser
{
    public class AppUserReadRepository : IAppUserReadRepository
    {
        public IQueryable<Entities.AppUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Entities.AppUser?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
