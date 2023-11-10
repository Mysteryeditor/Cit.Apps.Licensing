
using Cit.Apps.Licensing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class SubscriptionPlan:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingCycleType { get;set; }
        public decimal Cost { get; set; }
        [ForeignKey("ApplicationId")]
        public int ApplicationId { get; set; }
        public virtual ApplicationData ApplicationData{ get; set; }
    }
}
