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
    public class PurchaseOrderConfiguration : AuditableEntityConfiguration<PurchaseOrder>
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            base.Configure(builder);

            builder.ToTable("PurchaseOrders");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.OrderNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(po => po.OrderNumber)
                   .IsUnique();

            builder.HasIndex(po => po.Status);

            builder.HasOne(po => po.Supplier)
                   .WithMany(s => s.PurchaseOrders)
                   .HasForeignKey(po => po.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(po => po.Items)
                   .WithOne(i => i.PurchaseOrder)
                   .HasForeignKey(i => i.PurchaseOrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
