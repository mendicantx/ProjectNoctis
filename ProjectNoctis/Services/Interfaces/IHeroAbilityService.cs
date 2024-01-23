using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface IHeroAbilityService
    {
        List<HeroAbility> BuildHeroAbilityInfo(string name);
        List<HeroAbility> BuildHeroAbilityInfoBySchool(string school);
    }
}
