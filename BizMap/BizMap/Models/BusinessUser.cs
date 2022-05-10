using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.Models
{
    public class BusinessUser
    {

        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<BusinessStore> BusinessStores { get; set; }

    }
}
