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
    public class PropertyAgency
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public DocumentPermission DocumentPermission { get; set; }

        [Display(Name = "Property ID")]
        public long? PropertyId { get; set; }

        [Display(Name = "Property")]
        public Property Property { get; set; }

        [Display(Name = "Agency ID")]
        public long? AgencyId { get; set; }

        [Display(Name = "Agency")]
        public Agency Agency { get; set; }
    }
}
