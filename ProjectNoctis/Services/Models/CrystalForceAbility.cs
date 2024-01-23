using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class CrystalForceAbility
    {
        public SheetCrystalForceAbilities Info { get; set; }

        public Dictionary<string, List<SheetStatus>> AbilityStatuses { get; set; }

    }
}

