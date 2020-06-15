using ProjectNoctis.Domain.SheetDatabase.Models;
using System.Collections.Generic;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ISoulbreakRepository
    {
        List<SheetSoulbreaks> GetSoulbreaksByCharacterName(string tier, string name, int? index = null);

        List<SheetBraves> GetBravesByCharacterAndSoulbreak(string character, string soulbreak);

        List<SheetBursts> GetBurstsByCharacterAndSoulbreak(string character, string soulbreak);

        List<SheetSynchros> GetSynchrosByCharacterAndSoulbreak(string character, string soulbreak);

        List<SheetSoulbreaks> GetAllSoulbreaksByCharacterName(string name);

        List<SheetLimitBreaks> GetLimitBreaksByCharacterNameAndTier(string tier, string name, int? index = null);
    }
}