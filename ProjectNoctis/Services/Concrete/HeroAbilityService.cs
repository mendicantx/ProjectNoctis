using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectNoctis.Services.Concrete
{
    public class HeroAbilityService : IHeroAbilityService
    {
        private readonly IHeroAbilityRepository heroAbilityRepository;
        private readonly IStatusRepository statusRepository;

        public HeroAbilityService(IHeroAbilityRepository heroAbilityRepository, IStatusRepository statusRepository)
        {
            this.heroAbilityRepository = heroAbilityRepository;
            this.statusRepository = statusRepository;
        }



        public List<HeroAbility> BuildHeroAbilityInfo(string name)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var abilityMatch = heroAbilityRepository.GetHeroAbilityByCharacterName(name);

            if (abilityMatch.Count == 0)
            {
                return null;
            }

            var abilityList = new List<HeroAbility>();

            foreach (var abil in abilityMatch)
            {
                var ability = new HeroAbility();

                ability.Info = abil;
                var statuses = statusRegex.Matches(abil.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

                abilityList.Add(ability);
            }

            return abilityList;         
        }
        public List<HeroAbility> BuildHeroAbilityInfoBySchool(string school)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var abilityMatch = heroAbilityRepository.GetHeroAbilityBySchool(school);

            if (abilityMatch.Count == 0)
            {
                return null;
            }

            var abilityList = new List<HeroAbility>();

            foreach (var abil in abilityMatch)
            {
                var ability = new HeroAbility();

                ability.Info = abil;
                var statuses = statusRegex.Matches(abil.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

                abilityList.Add(ability);
            }

            return abilityList;         
        }


    }
}
