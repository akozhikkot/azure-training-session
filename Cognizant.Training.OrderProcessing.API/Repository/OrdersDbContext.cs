using Cognizant.Training.OrderProcessing.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.Training.OrderProcessing.API.Repository
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ideally should be on a new file.
            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<Order>().HasPartitionKey(x => x.PostCode);
            modelBuilder.Entity<Order>().OwnsMany(x => x.LineItems);
            modelBuilder.Entity<Order>().HasNoDiscriminator();
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
    }
}
