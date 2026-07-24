using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcureFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Infrastructure.Configurations.Common
{
    public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedAt)
               .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.UpdatedBy);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.DeletedBy);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
