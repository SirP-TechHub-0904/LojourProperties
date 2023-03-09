using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LojourProperties.Domain.Models
{
    public class PropertyFeature
    {
        public long Id { get; set; }

        [Display(Name = "Features Category ID")]
        public long FeaturesCategoryId { get; set; }

        [Display(Name = "Features Category")]
        public FeaturesCategory FeaturesCategory { get; set; }


        [Display(Name = "Property ID")]
        public long? PropertyId { get; set; }

        [Display(Name = "Property")]
        public Property Property { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
