using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LojourProperties.Domain.Models
{
    public class PropertyDocument
    {
        public long Id {get;set;}
        //public string Name { get;set;}
        public string Note { get;set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public bool Permitted { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Property ID")]
        public long? PropertyId { get; set; }

        [Display(Name = "Property")]
        public Property Property { get; set; }

        [Display(Name = "DocumentCategory ID")]
        public long? DocumentCategoryId { get; set; }

        [Display(Name = "Document Category")]
        public DocumentCategory DocumentCategory { get; set; }

    }
}
