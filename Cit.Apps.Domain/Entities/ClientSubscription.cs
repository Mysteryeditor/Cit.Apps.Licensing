using Cit.Apps.Licensing.Domain.Common;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class ClientSubscription : BaseEntity
    {
        public int ClientSubscriptionId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionPlanId { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTerminated { get; set; }

        public Client? Client { get; set; }
     
    }
}
