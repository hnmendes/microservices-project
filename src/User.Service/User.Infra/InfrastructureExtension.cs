using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using User.Domain.Contracts.Repositories;
using User.Infrastructure.Repositories;
using User.Application.Services.Contracts;
using User.Application.Services;

namespace User.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserDB")));                
            
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
