using Cit.Apps.Licensing.Domain.Common;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class SubscriptionFeature : BaseEntity
    {
        public int Id { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public SubscriptionPlan? SubscriptionPlan { get; set; }
    }
}
