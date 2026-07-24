using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Domain.Entities;
using ProcureFlow.Infrastructure.Configurations.Common;

namespace ProcureFlow.Infrastructure.Configurations
{
    public class PurchaseRequestItemConfiguration : AuditableEntityConfiguration<PurchaseRequestItem>
    {
        public override void Configure(EntityTypeBuilder<PurchaseRequestItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("PurchaseRequestItems");

            builder.HasKey(pri => pri.Id);

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.HasOne(pri => pri.PurchaseRequest)
                   .WithMany(pr => pr.Items)
                   .HasForeignKey(pri => pri.PurchaseRequestId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pri => pri.Product)
                   .WithMany(p => p.PurchaseRequestItems)
                   .HasForeignKey(pri => pri.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
