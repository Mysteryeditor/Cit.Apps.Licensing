namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class ClientSubscriptionsViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionPlanId { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTerminated { get; set; }
        public virtual ClientViewModel Client { get; set; }
        public virtual SubscriptionPlansViewModel SubscriptionPlan { get; set; }

    }
}
