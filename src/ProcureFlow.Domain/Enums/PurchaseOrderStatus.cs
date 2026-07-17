using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Domain.Enums
{
    public enum PurchaseOrderStatus
    {
        Pending = 1,
        Ordered = 2,
        PartiallyReceived = 3,
        Received = 4,
        Cancelled = 5
    }
}
