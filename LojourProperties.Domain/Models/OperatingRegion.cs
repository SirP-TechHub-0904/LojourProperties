using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LojourProperties.Domain.Models
{
    public class OperatingRegion
    {
        public long Id { get; set; }

        [Display(Name = "Display Title")]
        public string? DisplayTitle { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        public ICollection<CategoryLocation> Categories { get; set; }

    }
}
