using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Status
    {
        public string Name { get; set; }
        public string Effects { get; set; }
        public string StatusId { get; set; }
        public string DefaultDuration { get; set; }
        public string MindModifier { get; set; }
        public string ExclusiveStatus { get; set; }
    }
}
