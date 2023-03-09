using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class PropertyVideo
    {
        public long Id { get; set; }
        public string? VideoUrl { get; set; }
        public string? Key { get; set; }
        public string? Description { get; set; }
        public bool VR { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public long PropertyId { get; set; }
        public Property Property { get; set; }

    }
}
