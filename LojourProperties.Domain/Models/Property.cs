using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LojourProperties.Domain.Models.Enum;

namespace LojourProperties.Domain.Models
{
    public class Property
    {
        public long Id { get; set; }
        public string Key { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Property Ref ID")]
        public string? PropertyRefID { get; set; }

        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Full Description")]
        public string? FullDescription { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        

        [Display(Name = "Activity")]
        public long PropertyCategoryId { get; set; }

        [Display(Name = "Activity")]
        public PropertyCategory PropertyCategory { get; set; }

        [Display(Name = "Privacy Category")]
        public long PrivacyCategoryId { get; set; }

        [Display(Name = "Privacy Category")]
        public PrivacyCategory PrivacyCategory { get; set; }
 
        [Display(Name = "Property Agent")]
        public string AgentId { get; set; }

        [Display(Name = "Property Agent")]
        public Profile Agent { get; set; }

        public long? CityLocationId { get;set;}
        public CityLocation CityLocation { get;set;}

       

        [Display(Name = "Property Status")]
        public PropertyStatus PropertyStatus { get; set; }

        [Display(Name = "Activity Status")]
        public ActivityStatus ActivityStatus { get; set; }

        public ICollection<PropertyVideo> PropertyVideos { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
        public ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public ICollection<PropertyAgency> PropertyAgencies { get; set; }

         [Display(Name = "Property Linkage")]
        public PropertyLink PropertyLink { get;set;}

          [Display(Name = "Distress Property")]
        public bool Distress { get;set;}
    }
}
