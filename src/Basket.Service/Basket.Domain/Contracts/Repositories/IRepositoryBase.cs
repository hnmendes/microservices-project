using System.Threading.Tasks;

namespace Basket.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<string> Get(string key);
        Task<bool> Update(string key, string value);
        Task<bool> Delete(string key);
    }
}
