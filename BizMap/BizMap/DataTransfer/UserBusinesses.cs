using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.DataTransfer
{
    public record UserBusinesses: StatusResponse
    {
        public List<BusinessSummary> MyBusiness{ get; init; }
    }

    public record BusinessSummary
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Category { get; init; }
        public string Address { get; init; }
        public string BusinessCode { get; init; }
        public int Distance { get; init; }

    }

    public record BusinessInfo: StatusResponse
    {
        public BusinessInformation Information { get; set; }
    }

    public record BusinessInformation : BusinessSummary
    {
        public float Latitude { get; init; }
        public float Longitude { get; init; }
        public string Country { get; init; }
        public string State { get; init; }
        public string City { get; init; }
    }
}
