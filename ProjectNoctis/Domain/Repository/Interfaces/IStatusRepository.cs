using ProjectNoctis.Domain.SheetDatabase.Models;
using System.Collections.Generic;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IStatusRepository
    {
        SheetStatus GetStatusByName(string name);

        SheetOthers GetOthersByName(string name);

        Dictionary<string, List<SheetStatus>> GetStatusByNamesAndSource(string source, List<string> names, int counter, Dictionary<string, List<SheetStatus>> currentMatches = null);

        Dictionary<string, List<SheetOthers>> GetOthersByNamesAndSource(string source, Dictionary<string, List<SheetOthers>> currentMatches = null);

        Dictionary<string, List<SheetStatus>> GetStatusesByEffectText(string source, string effect);
    }
}