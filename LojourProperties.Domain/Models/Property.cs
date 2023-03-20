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
        public string PropertyRefID { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Full Description")]
        public string FullDescription { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Map Location")]
        public string Map { get; set; }

        [Display(Name = "Area Guide")]
        public string AreaGuide { get; set; }

        [Display(Name = "Property Possible Ussage")]
        public string PropertyUsage { get; set; }

        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Property Activity ID")]
        public long PropertyCategoryId { get; set; }

        [Display(Name = "Property Activity")]
        public PropertyCategory PropertyCategory { get; set; }

        [Display(Name = "Property Category ID")]
        public long PrivacyCategoryId { get; set; }

        [Display(Name = "Privacy Category")]
        public PrivacyCategory PrivacyCategory { get; set; }

        [Display(Name = "Property Category ID")]
        public long PropertyTypeId { get; set; }

        [Display(Name = "Property Category")]
        public PropertyType PropertyType { get; set; }

        [Display(Name = "Property Region ID")]
        public long OperatingRegionId { get; set; }

        [Display(Name = "Property Region")]
        public OperatingRegion OperatingRegion { get; set; }

        [Display(Name = "Property Agent ID")]
        public string AgentId { get; set; }

        [Display(Name = "Property Agent")]
        public Profile Agent { get; set; }



        [Display(Name = "land Area")]
        public string landArea { get; set; }

        [Display(Name = "Property Status")]
        public PropertyStatus PropertyStatus { get; set; }

        [Display(Name = "Activity Status")]
        public ActivityStatus ActivityStatus { get; set; }

        public ICollection<PropertyVideo> PropertyVideos { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
        public ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public ICollection<PropertyAgency> PropertyAgencies { get; set; }
    }
}
