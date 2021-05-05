using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    //This class will use EntityFrameworkCore SqlServer for handling Ordering microservices database persitance operations.
    public class OrderContext : DbContext
    {
        //Database property, Orders table.
        public DbSet<Order> Orders { get; set; }

        //Constructor
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        //This override method comes from the DbContext object default implementation.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //Iterating trough entries state inherited from the EntityBase class, since the user model has not been implemented,
            //we will specify the prebuild username "rdu".
            foreach (EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "rdu";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "rdu";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
