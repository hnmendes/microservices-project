using Basket.Domain.Contracts.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Basket.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly IDistributedCache _distributedCache;

        public RepositoryBase(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> Get(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);

            if(value is null) return null;

            return value;
        }

        public async Task<bool> Update(string key, string value)
        {
            try
            {
                await _distributedCache.SetStringAsync(key, value);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
                
                return true;
            }
            catch
            {
                return false;
            }            
            
        }
    }
}
