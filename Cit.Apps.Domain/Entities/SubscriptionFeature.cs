using Cit.Apps.Licensing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class SubscriptionFeature : BaseEntity
    {
        [ForeignKey("SubscriptionPlanId")]
        public int SubscriptionPlanId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
  
        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
