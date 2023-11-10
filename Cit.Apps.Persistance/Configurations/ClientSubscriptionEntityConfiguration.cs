using Cit.Apps.Licensing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class ClientSubscriptionEntityConfiguration: BaseEntityConfiguration<ClientSubscription>
    {
        public ClientSubscriptionEntityConfiguration()
        {
            this.ToTable("ClientSubscription");
            this.Property(p => p.ClientId).IsRequired().HasColumnType("int").HasColumnName("Client Id");
            //this.HasRequired(p => p.Client).WithMany().HasForeignKey(p => p.ClientId);

            this.Property(p => p.SubscriptionPlanId).HasColumnType("int").HasColumnName("Subscription Plan Id").IsRequired();
            //this.HasRequired(p => p.SubscriptionPlan).WithMany().HasForeignKey(p => p.SubscriptionPlanId);

            this.Property(p => p.IsActive).IsRequired().HasColumnType("bit").HasColumnName("IsActive");
            this.Property(p=>p.IsTerminated).IsRequired().HasColumnType("bit").HasColumnName("IsTerminated");
            this.Property(p => p.StartDate).HasColumnType("datetime").HasColumnName("Start Date").IsRequired();
            this.Property(p => p.EndDate).HasColumnType("datetime").HasColumnName("End Date").IsRequired();
        }
    }
}
