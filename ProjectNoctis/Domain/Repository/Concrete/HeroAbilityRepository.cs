using FuzzySharp;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class HeroAbilityRepository : IHeroAbilityRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public HeroAbilityRepository(IFfrkSheetContext ffrkSheetContext, Aliases aliases)
        {
            this.dbContext = ffrkSheetContext;
            this.aliases = aliases;
        }

        public List<SheetHeroAbilities> GetHeroAbilityByCharacterName(string name)
        {
            var charNames = dbContext.Characters.Select(x => x.Name.ToLower());

            if (aliases.AliasList.ContainsKey(name.ToLower()))
            {
                name = aliases.AliasList[name.ToLower()];
            }
            else if (!charNames.Contains(name.ToLower()))
            {
                name = charNames.OrderByDescending(x => Fuzz.PartialRatio(x, name.ToLower())).FirstOrDefault();
            }

            var ability = dbContext.HeroAbilities.Where(x => x.Character.ToLower() == name.ToLower()).OrderBy(x => x.HAVersion).ToList();

            return ability;
        }
        public List<SheetHeroAbilities> GetHeroAbilityBySchool(string school)
        {
            var ability = dbContext.HeroAbilities.Where(x => x.School.ToLower() == school.ToLower()).OrderBy(x => x.HAVersion).ToList();

            return ability;
        }
    }
}
