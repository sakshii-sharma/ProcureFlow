using ProcureFlow.Domain.Common;
using ProcureFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Domain.Entities
{
    public class StockTransaction : AuditableEntity
    {
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public int Quantity { get; set; }
        public StockTransactionType Type { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
        public string? Remarks { get; set; }

        //public Guid WarehouseId { get; set; }
        //public Guid ProductId { get; set; }
    }
}
