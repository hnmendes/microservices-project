using System.Threading.Tasks;

namespace Basket.Domain.Contracts.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<string> Get(string key);
        Task<bool> Update(string key, string value);
        Task<bool> Delete(string key);
    }
}
