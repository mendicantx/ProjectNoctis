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
    public class AbilityRepository : IAbilityRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public AbilityRepository(IFfrkSheetContext ffrkSheetContext, Aliases aliases)
        {
            this.dbContext = ffrkSheetContext;
            this.aliases = aliases;
        }

        public SheetAbilities GetAbilityByName(string name)
        {
            if (aliases.AliasList.ContainsKey(name.ToLower()))
            {
                name = aliases.AliasList[name.ToLower()];
            }

            var ability = dbContext.Abilities.FirstOrDefault(x => x.Name.ToLower() == name.ToLower() || x.JPName == name);

            if (ability == null)
            {
                ability = dbContext.Abilities.OrderByDescending(x => Fuzz.PartialRatio(x.Name.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return ability;
        }

        public List<SheetAbilities> GetHeroAbilityByCharacterName(string name)
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

            var ability = dbContext.Abilities.Where(x => x.Name.ToLower().Contains($"({name.ToLower()} only)")).ToList();

            return ability;
        }

        public List<SheetAbilities> GetAbilitiesBySchoolRank(string school, string rank, string element = null)
        {
            if (aliases.AliasList.ContainsKey(school.ToLower()))
            {
               school = aliases.AliasList[school.ToLower()];
            }

            var abilities = dbContext.Abilities.Where(x => x.School.ToLower() == school.ToLower() && x.Rarity == rank).ToList();

            if (element != null)
            {
                abilities = abilities.Where(x => x.Element.Split(",").Select(x => x.Trim().ToLower()).ToList().Contains(element.ToLower())).ToList();
            }   

            return abilities;
        }
    }
}
