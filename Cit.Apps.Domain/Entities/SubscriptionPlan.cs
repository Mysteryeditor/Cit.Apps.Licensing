
using Cit.Apps.Licensing.Domain.Common;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class SubscriptionPlan:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingCycleType { get;set; }
        public int ApplicationId { get; set; }
        public virtual ApplicationData Application { get; set; }
    }
}
