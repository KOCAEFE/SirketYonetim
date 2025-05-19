namespace SirketYonetim.Repositories.Abstract.AppUser
{
    public interface IAppUserReadRepository
    {
        IQueryable<Entities.AppUser> GetAll();
        Task<Entities.AppUser?> GetByIdAsync(string id);
    }
}
