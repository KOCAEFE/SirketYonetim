using SirketYonetim.Entities.Common;

namespace SirketYonetim.Repositories.Abstract
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        // CRUD işlemlerini burada tanımlanmalı for SOLID

        Task AddAsync(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task SaveChangesAsync();
    }
}
