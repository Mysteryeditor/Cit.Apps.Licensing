using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class SubscriptionFeatureConfiguration : BaseEntityConfiguration<SubscriptionFeature>
    {
        public SubscriptionFeatureConfiguration()
        {
            this.ToTable("SubscriptionFeature");
            this.Property(t => t.Name).HasColumnType("varchar(60)").IsRequired().HasColumnName("Name");
            this.Property(t => t.IsEnabled).HasColumnType("bit").IsRequired().HasColumnName("Is Enabled");
            //this.HasOptional(p => p.SubscriptionPlan).WithMany().HasForeignKey(p => p.Id);


        }
    }
}
