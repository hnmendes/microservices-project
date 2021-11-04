using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Domain.Contracts.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Update(string entityId, TEntity entity);
        Task<bool> Delete(string entityId);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(string entityId);
    }
}
