using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcureFlow.Infrastructure.Configurations.Common;

namespace ProcureFlow.Infrastructure.Configurations
{
    public class SupplierConfiguration : AuditableEntityConfiguration<Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);

            builder.ToTable("Suppliers");

            builder.HasKey("Id");

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(s => s.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex("Email")
                   .IsUnique();

            builder.HasIndex("Name");

            builder.HasMany(s => s.PurchaseOrders)
                   .WithOne(po => po.Supplier)
                   .HasForeignKey(po => po.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
