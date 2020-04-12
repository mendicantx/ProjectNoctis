using System.Collections.Generic;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ISoulbreakRepository
    {
        void UpdateSoulbreaksFromSheet(IList<SheetSoulbreaks> soulbreaks, IList<string> charNames);
        void UpdateRecordMateriaFromSheet(IList<SheetRecordMaterias> recordMaterias);
        void UpdateLegendMateriaFromSheet(IList<SheetLegendMaterias> legendMaterias);
        void UpdateBurstCommandsFromSheet(IList<SheetBursts> sheetBursts);
        void UpdateBraveCommandsFromSheet(IList<SheetBraves> sheetBraves);
        void UpdateSynchroCommandsFromSheet(IList<SheetSynchros> sheetSynchros);
        void UpdateSoulbreakStatuses();
        IList<DbSoulbreaks> GetSoulbreaksByCharacters(string charName);
    }
}