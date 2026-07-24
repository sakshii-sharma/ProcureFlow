using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcureFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Infrastructure.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");

            builder.HasKey(Warehouse => Warehouse.Id);

            builder.Property(w => w.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(w => w.Location)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasIndex(w => w.Name);

            builder.HasMany(w => w.Inventories)
                   .WithOne(i => i.Warehouse)
                   .HasForeignKey(w => w.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
