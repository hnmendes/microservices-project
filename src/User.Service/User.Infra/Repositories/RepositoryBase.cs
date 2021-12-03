using Microsoft.EntityFrameworkCore;
using User.Domain.Contracts.Repositories;
using User.Domain.Entities.Base;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly UserContext _db;

        public RepositoryBase(UserContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await _db.Set<TEntity>().AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
