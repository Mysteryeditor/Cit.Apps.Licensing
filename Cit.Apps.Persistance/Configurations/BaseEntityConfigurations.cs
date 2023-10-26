using Cit.Apps.Licensing.Domain.Common;
using System.Data.Entity.ModelConfiguration;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class BaseEntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public BaseEntityConfiguration()
        {

            this.Property(e => e.CreatedAt).HasColumnName("CreatedDate").IsRequired();
            this.Property(e => e.ModifiedAt).HasColumnName("ModifiedDate").IsRequired();
            this.HasOptional(p => p.CreatedByUser).WithMany().HasForeignKey(p => p.CreatedBy);
            this.HasOptional(p => p.ModifiedByUser).WithMany().HasForeignKey(p => p.ModifiedBy);
        }
    }
}
