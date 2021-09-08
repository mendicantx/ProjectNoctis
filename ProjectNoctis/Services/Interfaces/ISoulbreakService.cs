using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface ISoulbreakService
    {
        List<Soulbreak> BuildSoulbreakInfoFromCharNameAndTier(string tier, string character, int? index = null);

        List<Soulbreak> BuildSoulbreakInfoForAllCharSoulbreaksFromName(string name);

        List<LimitBreak> BuildLimitInfoFromCharNameAndTier(string tier, string character, int? index = null);

        List<Soulbreak> BuildSoulbreakInfoForAnimaWave(string wave);
    }
}
