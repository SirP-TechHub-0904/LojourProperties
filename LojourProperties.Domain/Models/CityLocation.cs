using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class CityLocation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CategoryLocationId { get;set; }
        public CategoryLocation CategoryLocation { get;set; }
    }
}
