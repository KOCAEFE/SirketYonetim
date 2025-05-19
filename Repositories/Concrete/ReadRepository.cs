using Microsoft.EntityFrameworkCore;
using SirketYonetim.Data;
using SirketYonetim.Entities.Common;
using SirketYonetim.Repositories.Abstract;

namespace SirketYonetim.Repositories.Concrete
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        protected readonly SirketYonetimContext _context;
        protected readonly DbSet<T> _dbSet;

        public ReadRepository(SirketYonetimContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
