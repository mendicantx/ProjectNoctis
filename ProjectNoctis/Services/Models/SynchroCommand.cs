using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class SynchroCommand
    {
        public SheetSynchros Info { get; set; }
        public Dictionary<string, List<SheetStatus>> SynchroStatuses { get; set; }
        public Dictionary<string, List<SheetOthers>> SynchroOthers { get; set; }
    }
}
