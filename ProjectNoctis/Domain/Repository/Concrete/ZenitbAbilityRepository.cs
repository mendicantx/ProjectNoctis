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
    public class ZenithAbilityRepository : IZenithAbilityRepository
    {
        private readonly IFfrkSheetContext dbContext;

        public ZenithAbilityRepository(IFfrkSheetContext ffrkSheetContext)
        {
            this.dbContext = ffrkSheetContext;
        }

        public List<SheetZenithAbilities> GetZenithAbilityBySoulbreakName(string soulBreakName)
        {
            var abilities = dbContext.ZenithAbilities.Where(x => x.Source.ToLower().Contains(soulBreakName.ToLower())).ToList();
            return abilities;
        }

    }
}
