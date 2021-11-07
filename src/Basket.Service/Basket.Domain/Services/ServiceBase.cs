using Basket.Domain.Contracts.Repositories;
using Basket.Domain.Contracts.Services;
using System.Threading.Tasks;

namespace Basket.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity> _repo;

        public ServiceBase(IRepositoryBase<TEntity> repo)
        {
            _repo = repo;
        }

        public async Task<string> Get(string key)
        {
            return await _repo.Get(key);
        }

        public async Task<bool> Update(string key, string value)
        {
            return await _repo.Update(key, value);
        }

        public async Task<bool> Delete(string key)
        {
            return await _repo.Delete(key);
        }
    }
}
