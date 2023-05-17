using LojourProperties.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LojourProperties.Domain.Models.Enum;
using System.Xml.Linq;

namespace LojourProperties.Domain.Dtos
{
    public class HomePropertyDto
    {
        public long Id { get; set; }
        public string Key { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        
        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "State")]
        public string? State { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }
        public string? Privacy { get; set; }
        public string? Category { get; set; }
        public ActivityStatus Activity { get; set; }
        public string? Image { get; set; }
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
        public ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public ICollection<PropertyAgency> PropertyAgencies { get; set; }

    }
}
