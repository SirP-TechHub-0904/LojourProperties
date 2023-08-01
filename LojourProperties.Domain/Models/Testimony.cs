using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Models
{
    public class Testimony
    {
        public long Id { get;set;}
        public string? Name { get;set;}
        public string? Location { get;set;}
        public string? Message { get;set;}
    }
}
