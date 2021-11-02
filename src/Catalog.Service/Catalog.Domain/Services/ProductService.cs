using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Contracts.Services;
using Catalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Domain.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        private readonly IProductRepository _db;

        public ProductService(IProductRepository db) : base(db)
        {
            _db = db;
        }

        public Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            return _db.GetByCategory(categoryName);
        }

        public Task<IEnumerable<Product>> GetByName(string name)
        {
            return _db.GetByName(name);
        }
    }
}
