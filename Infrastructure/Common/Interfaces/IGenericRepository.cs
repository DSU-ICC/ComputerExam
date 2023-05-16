namespace Infrastructure.Common.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Get();
        public IQueryable<TEntity> GetWithTracking();
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        public TEntity FindById(int id);
        public Task Create(TEntity item);
        public Task CreateRange(IEnumerable<TEntity> items);
        public Task Remove(int id);
        public Task Remove(TEntity item);
        public Task RemoveRange(IEnumerable<TEntity> items);
        public Task UpdateEntity(TEntity item);
        public Task Update(TEntity item);
    }
}
