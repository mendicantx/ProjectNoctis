using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class BurstCommand
    {
        public SheetBursts Info { get; set; }
        public Dictionary<string, List<SheetStatus>> BurstStatuses { get; set; }
        public Dictionary<string, List<SheetOthers>> BurstOthers { get; set; }
    }
}
