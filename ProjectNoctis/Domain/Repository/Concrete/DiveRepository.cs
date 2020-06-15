using FuzzySharp;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class DiveRepository : IDiveRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public DiveRepository(IFfrkSheetContext dbContext, Aliases aliases)
        {
            this.dbContext = dbContext;
            this.aliases = aliases;
        }

        public SheetLegendSpheres GetLegendDiveByCharacterName(string name)
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

            var legendDive = dbContext.LegendSpheres.FirstOrDefault(x => x.Character.ToLower() == name.ToLower());

            return legendDive;

        }

        public SheetRecordSpheres GetRecordDiveByCharacterName(string name)
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

            var recordDive = dbContext.RecordSpheres.FirstOrDefault(x => x.Character.ToLower() == name.ToLower());

            return recordDive;
        }

        public SheetRecordBoards GetRecordBoardByCharacterName(string name)
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

            var recordBoards = dbContext.RecordBoards.FirstOrDefault(x => x.Character.ToLower() == name.ToLower());

            return recordBoards;
        }
    }
}
