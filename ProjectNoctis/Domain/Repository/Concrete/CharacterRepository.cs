using FuzzySharp;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public CharacterRepository(IFfrkSheetContext context, Aliases aliases)
        {
            dbContext = context;
            this.aliases = aliases;
        }

        public SheetCharacters GetCharacterByName(string name)
        {
            name = aliases.ResolveAlias(name);
           
            var character = dbContext.Characters.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (character == null)
            {
                character = dbContext.Characters.OrderByDescending(x => Fuzz.PartialRatio(x.Name.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return character;
        }

        public List<SheetCharacters> GetCharacters()
        {
            var characters = dbContext.Characters.ToList();

            return characters;
        }

        public SheetCharacters GetCharacterById(string charId)
        {
            var character = dbContext.Characters.FirstOrDefault(x => x.CharacterId == charId);

            return character;
        }

        public SheetRecordSpheres GetCharacterRecordSphereByName(string name, bool exact = false)
        {
            if (aliases.AliasList.ContainsKey(name.ToLower()))
            {
                name = aliases.AliasList[name.ToLower()];
            }

            var recordSphere = dbContext.RecordSpheres.FirstOrDefault(x => x.Character.ToLower() == name.ToLower());

            if (recordSphere == null && !exact)
            {
                recordSphere = dbContext.RecordSpheres.OrderByDescending(x => Fuzz.PartialRatio(x.Character.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return recordSphere;
        }

        public SheetLegendSpheres GetCharacterLegendSpereByName(string name, bool exact = false)
        {
            if (aliases.AliasList.ContainsKey(name.ToLower()))
            {
                name = aliases.AliasList[name.ToLower()];
            }

            var legendSphere = dbContext.LegendSpheres.FirstOrDefault(x => x.Character.ToLower() == name.ToLower());

            if (legendSphere == null && !exact)
            {
                legendSphere = dbContext.LegendSpheres.OrderByDescending(x => Fuzz.PartialRatio(x.Character.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return legendSphere;
        }
    }
}
