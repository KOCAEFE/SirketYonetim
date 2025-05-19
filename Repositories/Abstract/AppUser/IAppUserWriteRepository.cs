namespace SirketYonetim.Repositories.Abstract.AppUser
{
    public interface IAppUserWriteRepository
    {
        Task<bool> AddAsync(Entities.AppUser user, string password);
        Task<bool> Update(Entities.AppUser user);
        Task<bool> Delete(string id);
    }
}
