using Cit.Apps.Licensing.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class UserEntityConfiguration:EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.ToTable("User");
            this.HasKey<int>(k => k.UserId);
            this.Property(p=>p.UserName).IsRequired().HasColumnType("varchar(30)").HasColumnName("User Name");
            this.Property(p=>p.FirstName).IsRequired().HasColumnType("varchar(30)").HasColumnName("First Name");
            this.Property(p => p.LastName).IsRequired().HasColumnType("varchar(30)").HasColumnName("Last Name");
            this.Property(p => p.PasswordHash).IsRequired().HasColumnType("nvarchar(max)").HasColumnName("Password Hash");
            this.Property(p => p.PasswordSalt).IsRequired().HasColumnType("nvarchar(max)").HasColumnName("Password Salt");
            this.Property(p => p.CreatedBy).HasColumnType("int").HasColumnName("Created By");
            this.Property(p => p.ModifiedBy).HasColumnType("int").HasColumnName("Modified By");
            this.Property(p => p.CreatedAt).HasColumnType("datetime").HasColumnName("Created At");
            this.Property(p => p.ModifiedAt).HasColumnType("datetime").HasColumnName("Modified At");
        }
    }
}
