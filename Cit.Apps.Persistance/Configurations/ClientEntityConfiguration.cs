using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class ClientEntityConfiguration:BaseEntityConfiguration<Client>
    {
        public ClientEntityConfiguration()
        {
            this.ToTable("Client");
            this.Property(p => p.ContactNumber).HasColumnType("bigint").HasColumnName("Contact Number").IsRequired();
            this.Property(p => p.ContactPerson).HasColumnType("varchar(40)").IsRequired().HasColumnName("Contact Person");
            this.Property(p => p.SubscriptionKey).HasColumnType("nvarchar(max)").IsRequired().HasColumnName("Subscription Key");


        }
    }
}
