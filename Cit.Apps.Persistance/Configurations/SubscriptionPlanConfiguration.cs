using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class SubscriptionPlanConfiguration : BaseEntityConfiguration<SubscriptionPlan>
    {
        public SubscriptionPlanConfiguration()
        {
            this.ToTable("SubscriptionPlan");
            this.HasKey<int>(e => e.SubscriptionPlanId);
            this.Property(e => e.Name).IsRequired().HasColumnType("varchar(30)").HasColumnName("Plan Name");
            this.Property(p => p.BillingCycleType).IsRequired().HasColumnType("varchar(20)").HasColumnName("Billing Cycle");
            this.Property(p => p.Description).IsRequired().HasColumnType("Varchar(200)").HasColumnName("Description");
            this.HasOptional(p => p.Application).WithMany().HasForeignKey(p => p.ApplicationId).WillCascadeOnDelete(false);

        }
    }
}
