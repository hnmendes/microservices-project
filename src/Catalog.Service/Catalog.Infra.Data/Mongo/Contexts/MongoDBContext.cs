using Catalog.Infra.Data.Mongo.Contexts.Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infra.Data.Mongo.Contexts
{
    public class MongoDBContext : IMongoDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }

        private readonly IConfiguration _configuration;
        public IClientSessionHandle Session { get; set; }

        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var dbName = _configuration["DatabaseSettings:DatabaseName"];

            _mongoClient = new MongoClient(connectionString);
            _db = _mongoClient.GetDatabase(dbName);

        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
