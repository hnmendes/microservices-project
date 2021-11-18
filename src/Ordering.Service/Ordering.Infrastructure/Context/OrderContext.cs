using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Entities.Base;

namespace Ordering.Infrastructure.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = entry.Entity.Id.ToString();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedOn = DateTime.Now;
                        entry.Entity.LastUpdatedBy = entry.Entity.Id.ToString();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
