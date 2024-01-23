using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface ICrystalForceAbilityService
    {
        List<CrystalForceAbility> BuildAbilityInfoBySoulbreakName(string soulbreakName);

    }
}
