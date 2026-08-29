using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = [];

        public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
