using Cit.Apps.Licensing.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class ClientSubscription : BaseEntity
    {
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        [ForeignKey("SubscriptionPlanId")]
        public int SubscriptionPlanId { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTerminated { get; set; }

        public virtual Client Client { get; set; }

        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
