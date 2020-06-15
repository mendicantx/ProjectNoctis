using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ICharacterRepository
    {
        SheetCharacters GetCharacterById(string charId);

        SheetCharacters GetCharacterByName(string name);

        List<SheetCharacters> GetCharacters();

        SheetRecordSpheres GetCharacterRecordSphereByName(string name, bool exact = false);

        SheetLegendSpheres GetCharacterLegendSpereByName(string name, bool exact = false);
    }
}