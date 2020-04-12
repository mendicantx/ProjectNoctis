using System.Collections.Generic;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ICharacterRepository
    {
        IList<DbCharacters> GetAllCharacters();
        IList<string> GetAllCharacterNames();
        DbCharacters GetCharacterByCharId(string characterId);
        DbCharacters GetCharacterByName(string name);
        void UpdateCharactersFromSheet(IList<SheetCharacters> characters);
        void UpdateLegendSpheresFromSheet(IList<SheetLegendSpheres> legendSpheres);
        void UpdateRecordSpheresFromSheet(IList<SheetRecordSpheres> recordSpheres);
        void UpdateRecordBoardsFromSheet(IList<SheetRecordBoards> recordBoards);

    }
}