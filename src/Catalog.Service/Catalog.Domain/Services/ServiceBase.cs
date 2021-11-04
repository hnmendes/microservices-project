using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _db;

        public ServiceBase(IRepositoryBase<TEntity> db)
        {
            _db = db;
        }

        public Task<TEntity> Create(TEntity entity)
        {
            return _db.Create(entity);
        }

        public Task<bool> Delete(string entityId)
        {
            return _db.Delete(entityId);
        }

        public Task<TEntity> Get(string entityId)
        {
            return _db.Get(entityId);
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return _db.GetAll();
        }

        public Task<bool> Update(string entityId, TEntity entity)
        {
            return _db.Update(entityId, entity);
        }
    }
}
