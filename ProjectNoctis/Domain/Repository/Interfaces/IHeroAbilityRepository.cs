using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IHeroAbilityRepository
    {
        List<SheetHeroAbilities> GetHeroAbilityByCharacterName(string name);
        List<SheetHeroAbilities> GetHeroAbilityBySchool(string school);
    }
}
