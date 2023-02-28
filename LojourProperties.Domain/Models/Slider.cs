using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class Slider
    {
        public long Id { get; set; }
        public string? Url { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Show { get; set; }

        public string? MainText { get; set; }
        public string? SubText { get; set; }
        public string? PlainText { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
    }
}
