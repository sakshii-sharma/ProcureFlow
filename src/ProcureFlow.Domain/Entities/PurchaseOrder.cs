using ProcureFlow.Domain.Common;
using ProcureFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Domain.Entities
{
    public class PurchaseOrder : AuditableEntity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public string OrderNumber { get; set; } = string.Empty;
        public PurchaseOrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }
}
