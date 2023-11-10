namespace Cit.Apps.Licensing.Application.ReadModels
{
    public class ClientSubscriptionReadModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionPlanId { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsTerminated { get; set; }
        public virtual ClientReadModel Client { get; set; }
        public virtual SubscriptionPlanReadModel SubscriptionPlan { get; set; }
    }
}
