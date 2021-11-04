using MongoDB.Driver;

namespace Catalog.Infra.Data.Mongo.Contexts.Contracts
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}
