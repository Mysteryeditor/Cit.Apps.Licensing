using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.Shared.Password;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Mapping;
using System.Reflection;

namespace Cit.Apps.Licensing.Persistence.Contexts
{
    public class DatabaseContext : DbContext
    {
        private readonly IPasswordService _passwordService;

        public DbSet<ApplicationData> Applications { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientSubscription> ClientSubscriptions { get; set; }

        public DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }

        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options, IPasswordService passwordService) :base(options) { _passwordService = passwordService; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<User>().Property(p => p.IsDefault).HasDefaultValue(false);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }

        private List<User> GetUsers()
        {
            List<User> userList = new();

            for (int index = 1; index <= 100; index++)
            {
                _passwordService.CreatePasswordHash($"Password{index}", out string PasswordHash, out string PasswordSalt);
                User user = new()
                {
                    Id = index,
                    UserName = $"User {index}",
                    LastName = $"LastName {index}",
                    FirstName = $"FirstName {index}",
                    PasswordHash =PasswordHash ,
                    PasswordSalt = PasswordSalt,
                    CreatedAt = DateTime.Now,
                    IsDefault = false
                };

                userList.Add(user);
            }

            return userList;
        }
    }
}
