using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace User.Api.Extensions
{
    public static class Host
    {
        public static IHost AddDatabaseMigrate<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    if (!TableExists<Domain.Entities.User>(context))
                    {
                        InvokeSeeder(seeder, context, services);
                        logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                    }
                    else
                    {
                        logger.LogInformation("Table already exists with context {DbContextName}", typeof(TContext).Name);
                    }

                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        AddDatabaseMigrate<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }

        private static bool TableExists<TEntity>(this DbContext context) where TEntity : class
        {
            try
            {
                context.Set<TEntity>().Count();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
