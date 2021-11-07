using Basket.Domain.Contracts.Repositories;
using Basket.Domain.Contracts.Services;
using Basket.Domain.Services;
using Basket.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infra.Data.Redis.Extensions
{
    public static class AddRedisStartup
    {
        public static IServiceCollection AddRedisContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(options =>
            {                
                options.Configuration = config["CacheSettings:ConnectionString"];
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            return services;
        }
    }
}
