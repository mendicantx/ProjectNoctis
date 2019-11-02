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
        DbEquipment GetEquipmentByName(string name);
        DbSkills GetSkillByNameAndValue(string name, int value);
        void UpdateCharactersFromSheet(IList<SheetCharacters> characters);
        IList<DbEquipment> UpdateOrAddEquipmentFromSheet(List<string> equipment);
        IList<DbSkills> UpdateOrAddSkillsFromSheet(Dictionary<string, int> skills);
    }
}