using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Soulbreak
    {
        public SheetSoulbreaks Info { get; set; }

        public IList<BraveCommand> BraveCommands { get; set; }
        public IList<BurstCommand> BurstCommands { get; set; }
        public IList<SynchroCommand> SynchroCommands { get; set; }
        public Dictionary<string, List<SheetStatus>> SoulbreakStatuses { get; set; }
        public Dictionary<string, List<SheetOthers>> SoulbreakOthers { get; set; }
    }
}
