using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ICrystalForceAbilityRepository
    {
        List<SheetCrystalForceAbilities> GetCrystalForceAbilityBySoulbreakName(string soulBreakName);
    }
}
