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
    public class Profile : IdentityUser
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Surname")]
        public string SurName { get; set; }
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]

      
        public string LastName { get; set; }
        [Display(Name = "FullName")]
        public string Fullname
        {
            get
            {
                return SurName + " " + FirstName + " " + LastName;
            }
        }
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }


         [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date Registered")]
        public DateTime DateRegistered { get; set; }
        [Display(Name = "Facebook Profile")]
        public string FacebookLink { get; set; }
        [Display(Name = "Twitter Profile")]
        public string TwitterLink { get; set; }
        [Display(Name = "Linkedin Profile")]
        public string LinkedinLink { get; set; }
        [Display(Name = "InstagramProfile")]
        public string InstagramLink { get; set; }
        [Display(Name = "Passport")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Operating Region ID")]
        public long OperatingRegionId { get; set; }
        [Display(Name = "Operating Region")]
        public OperatingRegion OperatingRegion { get; set; }
        [Display(Name = "Lojour Id")]
        public string LojourId { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
    }
}
