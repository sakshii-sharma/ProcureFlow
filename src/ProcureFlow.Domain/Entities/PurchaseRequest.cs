using ProcureFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Domain.Enums;

namespace ProcureFlow.Domain.Entities
{
    public class PurchaseRequest : AuditableEntity
    {
        public string RequestNumber { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty ;
        public PurchaseRequestStatus Status { get; set; }
        public string? Remarks { get; set; }
        public DateTime RequestedDate { get; set; }
        public ICollection<PurchaseRequestItem> Items { get; set; } = new List<PurchaseRequestItem>();
    }
}
