using Cit.Apps.Licensing.Domain.Common;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class Client:BaseEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public long ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string SubscriptionKey { get; set; }

    }
}
