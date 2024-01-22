using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetStatus
    {
        public string Name { get; set; }
        public string Effects { get; set; }
        public string StatusId { get; set; }
        public string DefaultDuration { get; set; }
        public string ExclusiveStatus { get; set; }
    }
}
