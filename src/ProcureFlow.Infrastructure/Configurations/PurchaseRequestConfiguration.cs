using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcureFlow.Domain.Entities;
using ProcureFlow.Infrastructure.Configurations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Infrastructure.Configurations
{
    public class PurchaseRequestConfiguration : AuditableEntityConfiguration<PurchaseRequest>
    {
        public override void Configure(EntityTypeBuilder<PurchaseRequest> builder)
        {
            base.Configure(builder);

            builder.ToTable("PurchaseRequests");

            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.RequestNumber)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(pr => pr.RequestedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pr => pr.Remarks)
                   .HasMaxLength(500);

            builder.HasIndex(pr => pr.RequestNumber)
                   .IsUnique();

            builder.HasIndex(pr => pr.Status);

            builder.HasMany(pr => pr.Items)
                   .WithOne(pri => pri.PurchaseRequest)
                   .HasForeignKey(pri => pri.PurchaseRequestId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
