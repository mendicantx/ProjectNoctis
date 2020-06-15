using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class LimitBreak
    {
        public SheetLimitBreaks Info { get; set; }

        public Dictionary<string, List<SheetStatus>> LimitStatuses { get; set; }

        public Dictionary<string, List<SheetOthers>> LimitOthers { get; set; }
    }
}
