using ProcureFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Domain.Entities
{
    public class PurchaseRequestItem : AuditableEntity
    {
        public Guid PurchaseRequestId { get; set; }
        public PurchaseRequest PurchaseRequest { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}

/* For Version 1 of the product : One Purchase Request -> One Product */