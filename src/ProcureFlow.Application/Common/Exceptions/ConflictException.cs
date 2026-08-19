using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Common.Exceptions
{    
    public sealed class ConflictException : Exception
    {
        public string Code { get; }

        public ConflictException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
