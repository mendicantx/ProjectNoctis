using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectNoctis.Services.Concrete
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityRepository abilityRepository;
        private readonly IStatusRepository statusRepository;

        public AbilityService(IAbilityRepository abilityRepository, IStatusRepository statusRepository)
        {
            this.abilityRepository = abilityRepository;
            this.statusRepository = statusRepository;
        }

        public Ability BuildAbilityInfo(string name)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var ability = new Ability();

            var abilityMatch = abilityRepository.GetAbilityByName(name);

            if(abilityMatch == null)
            {
                return null;
            }
            ability.Info = abilityMatch;
            var statuses = statusRegex.Matches(abilityMatch.Effects).Select(x => x?.Groups[1]?.Value).ToList();
            ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

            return ability;
        }

        public List<Ability> BuildHeroAbilityInfo(string name)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var abilityMatch = abilityRepository.GetHeroAbilityByCharacterName(name);

            if (abilityMatch.Count == 0)
            {
                return null;
            }

            var abilityList = new List<Ability>();

            foreach (var abil in abilityMatch)
            {
                var ability = new Ability();

                ability.Info = abil;
                var statuses = statusRegex.Matches(abil.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

                abilityList.Add(ability);
            }

            return abilityList;         
        }

        public List<Ability> BuildAbilityBySchoolInfo(string school, string rank, string element = null)
        {
            var abilities = abilityRepository.GetAbilitiesBySchoolRank(school, rank, element)
                .Select(x => new Ability() { Info = x})
                .ToList();

            return abilities;
        }
    }
}
