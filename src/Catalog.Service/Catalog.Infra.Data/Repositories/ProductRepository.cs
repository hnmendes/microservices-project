using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infra.Data.Mongo.Contexts.Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infra.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {        

        public ProductRepository(IMongoDbContext productContext, IConfiguration configuration) : base(productContext, configuration)
        {

        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {            
            var products = await _dbCollection.FindAsync(Builders<Product>.Filter.Eq(p => p.Category, categoryName));

            return await products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            var products = await _dbCollection.FindAsync(Builders<Product>.Filter.Eq(p => p.Name, name));

            return await products.ToListAsync();
        }
    }
}
