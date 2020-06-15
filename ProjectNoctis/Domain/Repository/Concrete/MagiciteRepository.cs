using FuzzySharp;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System.Linq;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class MagiciteRepository : IMagiciteRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public MagiciteRepository(IFfrkSheetContext context, Aliases aliases)
        {
            dbContext = context;
            this.aliases = aliases;
        }

        public SheetMagicites GetMagiciteByName(string name)
        {
            name = aliases.ResolveAlias(name);

            var magicite = dbContext.Magicites.FirstOrDefault(x => x.Name.ToLower() == name.ToLower() || x.JPName == name);

            if(magicite == null)
            {
                magicite = dbContext.Magicites.OrderByDescending(x => Fuzz.PartialRatio(x.Name.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return magicite;
        }
    }
}
