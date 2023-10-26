﻿using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Persistence.Configurations
{
    public class ApplicationEntityConfiguration : BaseEntityConfiguration<ApplicationData>
    {
        public ApplicationEntityConfiguration()
        {
            this.ToTable("Application");
            this.HasKey<int>(e => e.Id);
            this.Property(p => p.Name).HasColumnName("Name").HasColumnType("varchar(60)").IsRequired();
        }
    }
}
