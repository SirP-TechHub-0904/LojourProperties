using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class UserRegion
    {
         public long Id { get; set; }
        public string? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public long OperatingRegionId { get;set;}
        public OperatingRegion OperatingRegion { get;set;}
    }
}
