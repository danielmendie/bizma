using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.DataTransfer
{
    public record StatusResponse
    {
        public bool Status { get; init; }
        public string Message { get; init; }
    }
}
