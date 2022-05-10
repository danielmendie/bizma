using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.Models
{
    public class BusinessStore
    {
        public BusinessStore()
        {
            Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        [Key]
        public string Id { get; set; }
        public string BizCode { get; set; }
        public string BusinessName { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "float(10, 6)")]
        public float Latitute { get; set; }
        [Column(TypeName = "float(10, 6)")]
        public float Longitute { get; set; }
        public int Views { get; set; }


        public string BusinessUserId { get; set; }
        public virtual BusinessUser BusinessUser { get; set; }
    }

}
