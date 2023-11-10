namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class ClientSubscriptionWrapper
    {
        public List<ApplicationViewModel> applications { get; set; }
        public List<ClientViewModel> clients { get; set; }
        public List<SubscriptionPlansViewModel> plans { get; set; }
        public ClientSubscriptionsViewModel subscriptions { get; set; }
    }
}
