using Infrastructure.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sentry;
using System.Collections;
using System.Collections.Generic;
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

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public async Task Create(TEntity item)
        {
            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string create = "создании";
                SendExceptionInSentry(item, create, ex);
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
                string create = "обновленииTest";
                Exception ex = new Exception();
                SendExceptionInSentry(item, create, ex);
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string update = "обновлении";
                SendExceptionInSentry(item, update, ex);
            }
        }

        public async Task Update(TEntity item)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string update = "обновлении";
                SendExceptionInSentry(item, update, ex);
            }
        }

        public async Task Remove(TEntity item)
        {
            try
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string delete = "удалении";
                SendExceptionInSentry(item, delete, ex);
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
            catch (Exception ex)
            {
                string delete = "удалении";
                SendExceptionInSentry(item, delete, ex);                
            }
        }

        public async Task RemoveRange(IEnumerable<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        private static void SendExceptionInSentry(TEntity item, string methodName, Exception ex)
        {
            Type myType = item.GetType();

            var properties = myType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            string entity = "";
            foreach (var property in properties)
            {
                var value = property?.GetValue(item);
                if (value.GetType() == typeof(IEnumerable))
                {
                    var listObjects = value as IEnumerable;
                    foreach (var obj in listObjects)
                    {
                        var propertiesObj = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                        foreach (var propObj in propertiesObj)
                        {
                            var valueObj = property?.GetValue(obj);
                            entity += property.Name + ":" + valueObj + "\n";
                        }
                    }
                }
                entity += property.Name + ":" + value + "\n";
            }
            SentrySdk.CaptureMessage("Ошибка при " + methodName + " объкта " + myType.Name + "\n" + entity + "\n" + ex);
            throw new Exception();
        }
    }
}
