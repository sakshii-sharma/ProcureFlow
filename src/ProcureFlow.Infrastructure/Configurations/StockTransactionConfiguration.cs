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
    public class StockTransactionConfiguration : AuditableEntityConfiguration<StockTransaction>
    {
        public override void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            base.Configure(builder);

            builder.ToTable("StockTransactions");

            builder.HasKey(st => st.Id);

            builder.Property(st => st.ReferenceNumber)
                   .HasMaxLength(100);

            builder.Property(st => st.Remarks)
                   .HasMaxLength(500);

            builder.HasIndex(st => st.Type);

            builder.HasOne(st => st.Inventory)
                   .WithMany(i => i.StockTransactions)
                   .HasForeignKey(st => st.InventoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
