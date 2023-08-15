using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class CategoryLocation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageKey { get; set; }

        public long OperatingRegionId { get; set; }
        public OperatingRegion OperatingRegion { get; set; }

        public ICollection<CityLocation> Locations { get;set; }
    }
}
