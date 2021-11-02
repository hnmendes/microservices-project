using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Contracts.Services;
using Catalog.Domain.Services;
using Catalog.Infra.Data.Mongo.Contexts;
using Catalog.Infra.Data.Mongo.Contexts.Contracts;
using Catalog.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infra.Data.Mongo.Extensions
{
    public static class AddMongoStartup
    {
        public static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IMongoDbContext, MongoDBContext>();            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;

        }

    }
}
