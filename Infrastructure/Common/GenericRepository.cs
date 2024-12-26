using Infrastructure.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public async Task Create(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<TEntity> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntity(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRange(IEnumerable<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
