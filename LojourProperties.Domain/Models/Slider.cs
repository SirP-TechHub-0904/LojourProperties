using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LojourProperties.Domain.Models
{
    public class Slider
    {
        public long Id { get; set; }
        public string? Url { get; set; }
        public string? Key { get; set; }

        [Display(Name = "Is Video")]
        public bool IsVideo { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public bool Show { get; set; }

        [Display(Name = "Main Text")]
        public string? MainText { get; set; }

        [Display(Name = "Sub Text")]
        public string? SubText { get; set; }

        [Display(Name = "Plain Text")]
        public string? PlainText { get; set; }

        [Display(Name = "Button Text")]
        public string? ButtonText { get; set; }

        [Display(Name = "Button Link")]
        public string? ButtonLink { get; set; }
    }
}
