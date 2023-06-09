using Infrastructure.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sentry;
using System.Reflection;

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

        public IQueryable<TEntity> GetWithTracking()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task Create(TEntity item)
        {
            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                string create = "создании";
                SendExceptionInSentry(item, create);
                throw new Exception();
            }
        }

        public async Task CreateRange(IEnumerable<TEntity> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntity(TEntity item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                string update = "обновлении";
                SendExceptionInSentry(item, update);
                throw new Exception();
            }
        }

        public async Task Update(TEntity item)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                string update = "обновлении";
                SendExceptionInSentry(item, update);
                throw new Exception();
            }
        }

        public async Task Remove(TEntity item)
        {
            try
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                string delete = "удалении";
                SendExceptionInSentry(item, delete);
                throw new Exception();
            }
        }

        public async Task Remove(int id)
        {
            var item = await _dbSet.FindAsync(id);
            try
            {
                if (item != null)
                {
                    _dbSet.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                string delete = "удалении";
                SendExceptionInSentry(item, delete);
                throw new Exception();
            }
        }

        public async Task RemoveRange(IEnumerable<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        private static void SendExceptionInSentry(TEntity item, string methodName)
        {
            Type myType = item.GetType();

            var properties = myType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            string entity = "";
            foreach (var property in properties)
            {
                var value = property?.GetValue(item);
                entity += property.Name + ":" + value + "\n";
            }
            SentrySdk.CaptureMessage("Ошибка при " + methodName + " объкта " + myType.Name + "\n" + entity);
        }
    }
}
