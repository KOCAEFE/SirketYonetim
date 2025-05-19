using Microsoft.EntityFrameworkCore;
using SirketYonetim.Data;
using SirketYonetim.Entities.Common;
using SirketYonetim.Repositories.Abstract;

namespace SirketYonetim.Repositories.Concrete
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly SirketYonetimContext _context;
        protected readonly DbSet<T> _dbSet;

        public WriteRepository(SirketYonetimContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = null;
            await _dbSet.AddAsync(entity);
        }

        public Task Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
