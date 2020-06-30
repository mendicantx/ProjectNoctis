using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IAbilityRepository
    {
        SheetAbilities GetAbilityByName(string name);

        List<SheetAbilities> GetHeroAbilityByCharacterName(string name);

        List<SheetAbilities> GetAbilitiesBySchoolRank(string school, string rank, string element = null);
    }
}
