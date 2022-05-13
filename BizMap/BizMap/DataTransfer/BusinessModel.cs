using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.DataTransfer
{
    public record BusinessModel
    {
        [Required(ErrorMessage ="Business name required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Business address required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Country required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "State required")]
        public string State { get; set; }
        [Required(ErrorMessage = "City required")]
        public string City { get; set; }
        [Required]
        public float? Latitude { get; init; }
        [Required]
        public float? Longitude { get; init; }
        [Required(ErrorMessage = "Category required")]
        public string Category { get; init; }

        public string FullName { get; init; }
        public string Gender { get; init; }
    }
}
