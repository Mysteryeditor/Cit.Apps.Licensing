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

        public DatabaseContext(DbContextOptions options, IPasswordService passwordService) : base(options) { _passwordService = passwordService; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<User>().Property(p => p.IsDefault).HasDefaultValue(false);
            modelBuilder.Entity<ApplicationData>().HasData(GetApplications());
            modelBuilder.Entity<Client>().HasData(GetClients());
            modelBuilder.Entity<SubscriptionPlan>().HasData(GetPlans());
            modelBuilder.Entity<SubscriptionFeature>().HasData(GetFeatures());
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
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,
                    CreatedAt = DateTime.Now,
                    IsDefault = false
                };
                userList.Add(user);
            }
            return userList;
        }

        private static List<ApplicationData> GetApplications()
        {
            return Enumerable.Range(1, 10).Select(i => new ApplicationData 
            { Id = i, Name = $"Application{i}", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now,CreatedBy=i,ModifiedBy=i }).ToList();
        }

        private static List<Client> GetClients()
        {
            return Enumerable.Range(1,5).Select(i=>new Client
            {
                Id = i,
                ContactNumber = 954678913+i,
                Name=$"Client{i}",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = i,
                ModifiedBy = i
            }).ToList();
        }

        private static List<SubscriptionPlan> GetPlans()
        {
            return Enumerable.Range(1, 4).Select(p => new SubscriptionPlan
            {
                Id = p,
                BillingCycleType = "Monthly",
                Cost = 2000 + (p * 10),
                CreatedAt= DateTime.Now,
                CreatedBy=p,
                ModifiedBy=p,
                ModifiedAt=DateTime.Now,
                ApplicationId=p,
                Name=$"Plan{p}",
                Description =$"Description{p }"
            }).ToList();
        }

        private static List<SubscriptionFeature> GetFeatures()
        {
            return Enumerable.Range(1, 3).Select(p => new SubscriptionFeature
            {
                Id = p,
                Name=$"Featire {1}",
                SubscriptionPlanId=p,
                IsEnabled=false,
                CreatedAt = DateTime.Now,
                CreatedBy = p,
                ModifiedBy = p,
                ModifiedAt = DateTime.Now,

            }).ToList();
        }
    }
}
