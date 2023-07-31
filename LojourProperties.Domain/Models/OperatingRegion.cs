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
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        
        [Display(Name = "Region Of Operation")]
        public string RegionOfOperation
        {

            get
            {
                //return City + " / " + State + " / " + Zone + " / " + Region +" / " + Country;
                return Location + " in " + State;
            }
        }


    }
}
