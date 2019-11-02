using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class SoulbreakRepository : ISoulbreakRepository
    {
        private readonly FFRecordContext dbContext;
        public SoulbreakRepository(FFRecordContext context)
        {
            dbContext = context;
        }

        public DbSoulbreaks GetSoulbreakById(string id)
        {
            return dbContext.Soulbreaks.FirstOrDefault(x => x.SoulbreakId == id);
        }

        public void UpdateSoulbreaksFromSheet(IList<SheetSoulbreaks> soulbreaks, IList<string> charNames)
        {
            var soulbreakList = new List<DbSoulbreaks>();
            foreach (var soulbreak in soulbreaks.Where(x => x.Tier != "RW" && charNames.Contains(x.Character)))
            {
                var soulbreakToUpdate = GetSoulbreakById(soulbreak.SoulbreakId);
                if(soulbreakToUpdate == null)
                {
                    soulbreakList.Add(new DbSoulbreaks
                    {
                        Anima = soulbreak.Anima,
                        Character = soulbreak.Character,
                        Effects = soulbreak.Effects,
                        Element = soulbreak.Element,
                        Formula = soulbreak.Formula,
                        JPName = soulbreak.JPName,
                        Multiplier = soulbreak.Multiplier,
                        Name = soulbreak.Name,
                        Points = soulbreak.Points,
                        Realm = soulbreak.Realm,
                        Relic = soulbreak.Relic,
                        SoulbreakBonus = soulbreak.SoulbreakBonus,
                        SoulbreakId = soulbreak.SoulbreakId,
                        Target = soulbreak.Target,
                        Tier = soulbreak.Tier,
                        Type = soulbreak.Type
                    });

                   
                }
                else
                {
                    soulbreakToUpdate.Anima = soulbreak.Anima;
                    soulbreakToUpdate.Character = soulbreak.Character;
                    soulbreakToUpdate.Effects = soulbreak.Effects;
                    soulbreakToUpdate.Element = soulbreak.Element;
                    soulbreakToUpdate.Formula = soulbreak.Formula;
                    soulbreakToUpdate.JPName = soulbreak.JPName;
                    soulbreakToUpdate.Multiplier = soulbreak.Multiplier;
                    soulbreakToUpdate.Name = soulbreak.Name;
                    soulbreakToUpdate.Points = soulbreak.Points;
                    soulbreakToUpdate.Realm = soulbreak.Realm;
                    soulbreakToUpdate.Relic = soulbreak.Relic;
                    soulbreakToUpdate.SoulbreakBonus = soulbreak.SoulbreakBonus;
                    soulbreakToUpdate.SoulbreakId = soulbreak.SoulbreakId;
                    soulbreakToUpdate.Target = soulbreak.Target;
                    soulbreakToUpdate.Tier = soulbreak.Tier;
                    soulbreakToUpdate.Type = soulbreak.Type;
                }
            }
            dbContext.UpdateRange(soulbreakList);
            dbContext.SaveChanges();
        }
    }
}
