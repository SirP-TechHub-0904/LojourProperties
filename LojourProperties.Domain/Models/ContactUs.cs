using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class ContactUs
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Fullname { get; set; }
        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
