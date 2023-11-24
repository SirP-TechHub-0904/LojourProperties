using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class Newsletter
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
