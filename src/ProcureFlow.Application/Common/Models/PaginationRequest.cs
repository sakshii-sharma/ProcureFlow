using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Common.Models
{
    public class PaginationRequest
    {
        private const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, MaxPageSize);
        }
    }
}
