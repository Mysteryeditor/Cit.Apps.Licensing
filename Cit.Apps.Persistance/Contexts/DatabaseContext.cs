using Cit.Apps.Licensing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cit.Apps.Licensing.Persistence.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ApplicationData> Applications { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientSubscription> ClientSubscriptions { get; set; }

        public DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }

        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}
