using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class BraveCommand
    {
        public SheetBraves Info { get; set; }
        public Dictionary<string, List<SheetStatus>> BraveStatuses { get; set; }
        public Dictionary<string, List<SheetOthers>> BraveOthers { get; set; }
    }
}
