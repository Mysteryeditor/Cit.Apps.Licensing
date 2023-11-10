using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class SubscriptionPlanConfiguration : BaseEntityConfiguration<SubscriptionPlan>
    {
        public SubscriptionPlanConfiguration()
        {
            this.ToTable("SubscriptionPlan");
            this.Property(e => e.Name).IsRequired().HasColumnType("varchar(30)").HasColumnName("Plan Name");
            this.Property(e => e.Cost).IsRequired().HasColumnType("smallmoney").HasColumnName("Cost");
            this.Property(p => p.BillingCycleType).IsRequired().HasColumnType("varchar(20)").HasColumnName("Billing Cycle");
            this.Property(p => p.Description).IsRequired().HasColumnType("Varchar(200)").HasColumnName("Description");
            //this.HasOptional(p => p.ApplicationData).WithMany().HasForeignKey(p => p.Id).WillCascadeOnDelete(false);
        }
    }
}
