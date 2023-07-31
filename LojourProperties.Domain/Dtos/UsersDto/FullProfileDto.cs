using System.ComponentModel.DataAnnotations;
using static LojourProperties.Domain.Models.Enum;

namespace LojourProperties.Domain.Dtos.UsersDto
{
    public class FullProfileDto
    {
        public string Id { get; set; }

        [Display(Name = "FullName")]
        public string? FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "Passport")]
        public string? PassportFilePathUrl { get; set; }
    }
}
