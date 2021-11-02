using Catalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Domain.Contracts.Services
{
    public interface IProductService : IServiceBase<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryName);
        Task<IEnumerable<Product>> GetByName(string name);
    }
}
