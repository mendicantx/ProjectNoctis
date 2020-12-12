using Discord;
using Discord.Commands;
using FuzzySharp;
using Google.Apis.Sheets.v4.Data;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class SoulbreakRepository : ISoulbreakRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public SoulbreakRepository(IFfrkSheetContext context, Aliases aliases)
        {
            dbContext = context;
            this.aliases = aliases;
        }

        public List<SheetSoulbreaks> GetSoulbreaksByCharacterName(string tier, string name, int? index = null)
        {
            var charNames = dbContext.Characters.Select(x => x.Name.ToLower());

            name = aliases.ResolveAlias(name);

            if (!charNames.Contains(name.ToLower()))
            {
                name = charNames.OrderByDescending(x => Fuzz.PartialRatio(x, name.ToLower())).FirstOrDefault();
            }

            List<SheetSoulbreaks> soulbreaks = new List<SheetSoulbreaks>(); 
            if (tier == "g")
            {
                soulbreaks = dbContext.Soulbreaks.Where(x => (x.Character.ToLower() == name.ToLower() || x.SoulbreakId == name) && (x.Tier == "Glint" || x.Tier == "Glint+")).ToList();
            }
            else if (tier == "brave")
            {
                soulbreaks = dbContext.Soulbreaks.Where(x => (x.Character.ToLower() == name.ToLower() || x.SoulbreakId == name) && x.Effects.Contains("[Brave Mode]")).ToList();
            }
            else
            {
                soulbreaks = dbContext.Soulbreaks.Where(x => (x.Character.ToLower() == name.ToLower() || x.SoulbreakId == name) && x.Tier == tier).ToList();
            }
           
            if (index != null)
            {
                if (index.Value < soulbreaks.Count() && index > -1)
                {
                    return new List<SheetSoulbreaks>() { soulbreaks[index.Value] };
                }
            }

            return soulbreaks;
        }

        public List<SheetSoulbreaks> GetAllSoulbreaksByCharacterName(string name)
        {
            var charNames = dbContext.Characters.Select(x => x.Name.ToLower());

            name = aliases.ResolveAlias(name);

            if (!charNames.Contains(name.ToLower()))
            {
                name = charNames.OrderByDescending(x => Fuzz.PartialRatio(x, name.ToLower())).FirstOrDefault();
            }

            var soulbreaks = dbContext.Soulbreaks.Where(x => x.Character.ToLower() == name.ToLower() && x.Tier != "RW").ToList();

            return soulbreaks;
        }

        public List<SheetLimitBreaks> GetLimitBreaksByCharacterNameAndTier(string tier, string name, int? index = null)
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

            List<SheetLimitBreaks> limits = new List<SheetLimitBreaks>();
            
            
            limits = dbContext.LimitBreaks.Where(x => (x.Character.ToLower() == name.ToLower() || x.ID == name) && x.Tier == tier).ToList();
            

            if (index != null)
            {
                if (index.Value < limits.Count() && index > -1)
                {
                    return new List<SheetLimitBreaks>() { limits[index.Value] };
                }
            }

            return limits;
        }

        public List<SheetBraves> GetBravesByCharacterAndSoulbreak(string character, string soulbreak)
        {
            var braves = dbContext.Braves.Where(x => x.Character == character && x.Source == soulbreak).ToList();

            return braves;
        }

        public List<SheetBursts> GetBurstsByCharacterAndSoulbreak(string character, string soulbreak)
        {
            var bursts = dbContext.Bursts.Where(x => x.Character == character && x.Source == soulbreak).ToList();

            return bursts;
        }

        public List<SheetSynchros> GetSynchrosByCharacterAndSoulbreak(string character, string soulbreak)
        {
            var synchros = dbContext.Synchros.Where(x => x.Character == character && x.Source == soulbreak).ToList();

            return synchros;
        }
    }
}
