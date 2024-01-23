using System.Security.Cryptography.X509Certificates;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectNoctis.Services.Concrete
{
    public class CrystalForceAbilityService : ICrystalForceAbilityService
    {
        private readonly ICrystalForceAbilityRepository crystalForceAbilityRepository;
        private readonly IStatusRepository statusRepository;

        public CrystalForceAbilityService(ICrystalForceAbilityRepository crystalForceAbilityRepository, IStatusRepository statusRepository)
        {
            this.crystalForceAbilityRepository = crystalForceAbilityRepository;
            this.statusRepository = statusRepository;
        }


        public List<CrystalForceAbility> BuildAbilityInfoBySoulbreakName(string soulbreakName)
        {
            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var abilityMatch = crystalForceAbilityRepository.GetCrystalForceAbilityBySoulbreakName(soulbreakName);

            if (abilityMatch.Count == 0)
            {
                return new List<CrystalForceAbility>();
            }

            var abilityList = new List<CrystalForceAbility>();

            foreach (var abil in abilityMatch)
            {
                var ability = new CrystalForceAbility();

                ability.Info = abil;
                var statuses = statusRegex.Matches(abil.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                ability.AbilityStatuses = statusRepository.GetStatusByNamesAndSource(ability.Info.Name, statuses, 0);

                abilityList.Add(ability);
            }

            return abilityList;         
        }
    }
}
