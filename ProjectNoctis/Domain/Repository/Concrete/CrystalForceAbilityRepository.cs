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
    public class CrystalForceAbilityRepository : ICrystalForceAbilityRepository
    {
        private readonly IFfrkSheetContext dbContext;

        public CrystalForceAbilityRepository(IFfrkSheetContext ffrkSheetContext)
        {
            this.dbContext = ffrkSheetContext;
        }

        public List<SheetCrystalForceAbilities> GetCrystalForceAbilityBySoulbreakName(string soulBreakName)
        {
            var abilities = dbContext.CrystalForceAbilities.Where(x => x.Source.ToLower().Trim() == soulBreakName.ToLower().Trim()).ToList();
            return abilities;
        }

    }
}
