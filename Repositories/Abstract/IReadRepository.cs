using SirketYonetim.Entities.Common;

namespace SirketYonetim.Repositories.Abstract
{
    public interface IReadRepository<T> : IRepository<T> where T: BaseEntity
    {
        // SELECT işlemleri Read içerisinden tanımlanmalı for SOLID

        // Sorgu için IQueryable (where). Eğer Memory ise IEnumable || List mantıklı olacaktır

        IQueryable<T> GetAll();

        Task<T?> GetByIdAsync(Guid id);
    }
}
