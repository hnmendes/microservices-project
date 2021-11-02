using Catalog.Domain.Contracts.Repositories;
using Catalog.Infra.Data.Mongo.Contexts.Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly IMongoDbContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;
        private readonly IConfiguration _configuration;

        public BaseRepository(IMongoDbContext mongoContext, IConfiguration configuration)
        {
            _configuration = configuration;

            var keyEntityName = "DatabaseSettings:" + (typeof(TEntity).Name);
            var collectionName = _configuration[keyEntityName];

            _mongoContext = mongoContext;            
            _dbCollection = _mongoContext.GetCollection<TEntity>(collectionName);            
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            await _dbCollection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<bool> Delete(string entityId)
        {
            var objectId = new ObjectId(entityId);
            var result = await _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));

            return result.DeletedCount > 0;
        }

        public async Task<TEntity> Get(string entityId)
        {
            var objectId = new ObjectId(entityId);

            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<bool> Update(string entityId, TEntity entity)
        {
            var objectId = new ObjectId(entityId);
            var result = await _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId), entity);

            return result.ModifiedCount > 0;
        }
    }
}
