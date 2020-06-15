using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetOthers
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Element { get; set; }
        public string Time { get; set; }
        public string Effects { get; set; }
        public string Multiplier { get; set; }
        public string Formula { get; set; }
        public string School { get; set; }
        public string OtherId { get; set; }
        public string SB { get; set; }

        public Dictionary<string, List<SheetStatus>> OtherStatuses { get; set; }
    }
}
