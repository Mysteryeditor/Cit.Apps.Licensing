namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class SubscriptionPlansViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingCycleType { get; set; }
        public decimal Cost { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
