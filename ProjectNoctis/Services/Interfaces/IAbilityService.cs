using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface IAbilityService
    {
        Ability BuildAbilityInfo(string name);

        List<Ability> BuildAbilityBySchoolInfo(string school, string rank, string element = null);

        List<Ability> BuildHeroAbilityInfo(string name);
    }
}
