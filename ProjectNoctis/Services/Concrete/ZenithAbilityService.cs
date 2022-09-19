using System.Security.Cryptography.X509Certificates;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectNoctis.Services.Concrete
{
    public class ZenithAbilityService : IZenithAbilityService
    {
        private readonly IZenithAbilityRepository zenithAbilityRepository;
        private readonly IStatusRepository statusRepository;

        public ZenithAbilityService(IZenithAbilityRepository zenithAbilityRepository, IStatusRepository statusRepository)
        {
            this.zenithAbilityRepository = zenithAbilityRepository;
            this.statusRepository = statusRepository;
        }


        public List<ZenithAbility> BuildAbilityInfoBySoulbreakName(string soulbreakName)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var abilityMatch = zenithAbilityRepository.GetZenithAbilityBySoulbreakName(soulbreakName);

            if (abilityMatch.Count == 0)
            {
                return new List<ZenithAbility>();
            }

            var abilityList = new List<ZenithAbility>();

            foreach (var abil in abilityMatch)
            {
                var ability = new ZenithAbility();

                ability.Info = abil;
                var statuses = statusRegex.Matches(abil.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

                abilityList.Add(ability);
            }

            return abilityList;         
        }
    }
}
