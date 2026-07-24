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
    public class InventoryConfiguration : AuditableEntityConfiguration<Inventory>
    {
        public override void Configure(EntityTypeBuilder<Inventory> builder) 
        {
            base.Configure(builder);

            builder.ToTable("Inventories");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.Property(i => i.MinimumStock)
                   .IsRequired();

            builder.Property(i => i.ReorderLevel)
                   .IsRequired();

            builder.HasIndex(i => new { i.ProductId, i.WarehouseId })
                   .IsUnique();

            // many - one | M StockTransactions to 1 Inventory
            builder.HasMany(i => i.StockTransactions)
                   .WithOne(st => st.Inventory)
                   .HasForeignKey(st => st.InventoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // one - many | 1 Procduct to M Inventories
            builder.HasOne(i => i.Product)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // one - many | 1 Warehouse to M Inventories
            builder.HasOne(i => i.Warehouse)
                   .WithMany(w => w.Inventories)
                   .HasForeignKey(i => i.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
