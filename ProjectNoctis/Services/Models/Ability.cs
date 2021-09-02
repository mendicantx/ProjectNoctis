using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Ability
    {
        public SheetAbilities Info { get; set; }

        public Dictionary<string, List<SheetStatus>> AbilityStatuses { get; set; }

        public bool IsHeroAbility() {
            return Info.Name.ToLower().Trim().EndsWith("only)");
        }
    }
}

