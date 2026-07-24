using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProcureFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Infrastructure.Configurations.Common;

namespace ProcureFlow.Infrastructure.Configurations
{
    public class PurchaseOrderItemConfiguration : AuditableEntityConfiguration<PurchaseOrderItem>
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("PurchaseOrderItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.Property(i => i.UnitPrice)
                   .HasPrecision(18, 2);

            builder.HasOne(i => i.Product)
                   .WithMany(p => p.PurchaseOrderItems)
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.PurchaseOrder)
                   .WithMany(po => po.Items)
                   .HasForeignKey(i => i.PurchaseOrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
