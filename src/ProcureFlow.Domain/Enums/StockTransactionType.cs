using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Domain.Enums
{
    public enum StockTransactionType
    {
        StockIn = 1,
        StockOut = 2,
        Adjustment = 3,
        Transfer = 4
    }
}
