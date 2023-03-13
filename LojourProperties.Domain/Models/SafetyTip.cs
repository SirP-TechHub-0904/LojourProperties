using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class SafetyTip
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SortOrder { get; set; }
        public bool Show { get; set; }
    }
}
