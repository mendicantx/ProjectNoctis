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
    public class MagiciteRepository : IMagiciteRepository
    {
        private readonly FFRecordContext dbContext;
        public MagiciteRepository(FFRecordContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbMagicite GetMagiciteByMagiciteId(string id)
        {
            return dbContext.Magicites.FirstOrDefault(x => x.MagiciteId == id);
        }


        public void AddOrUpdateMagicitesFromSheet(IList<SheetMagicites> magicites)
        {

            var dbMagicites = new List<DbMagicite>();

            foreach (var magicite in magicites)
            {
                var matchedMagicite = GetMagiciteByMagiciteId(magicite.MagiciteId);

                if (matchedMagicite != null)
                {
                    matchedMagicite.Effects = magicite.Effects;
                    matchedMagicite.Element = magicite.Element;
                    matchedMagicite.Formula = magicite.Formula;
                    matchedMagicite.JPName = magicite.JPName;
                    matchedMagicite.MagiciteId = magicite.MagiciteId;
                    matchedMagicite.MagiciteUltra = magicite.MagiciteUltra;
                    matchedMagicite.Multiplier = magicite.Multiplier;
                    matchedMagicite.Name = magicite.Name;
                    matchedMagicite.Rarity = magicite.Rarity;
                    matchedMagicite.Realm = magicite.Realm;
                    matchedMagicite.Time = magicite.Time;
                    matchedMagicite.Type = magicite.Type;
                    matchedMagicite.UltraElement = magicite.UltraElement;

                    //TODO: Add magicite passives to be updated
                }
                else
                {
                    dbMagicites.Add(new DbMagicite
                    {
                        Effects = magicite.Effects,
                        Element = magicite.Element,
                        Formula = magicite.Formula,
                        JPName = magicite.JPName,
                        MagiciteId = magicite.MagiciteId,
                        MagiciteUltra = magicite.MagiciteUltra,
                        Multiplier = magicite.Multiplier,
                        Name = magicite.Name,
                        Rarity = magicite.Rarity,
                        Realm = magicite.Realm,
                        Time = magicite.Time,
                        Type = magicite.Type,
                        UltraElement = magicite.UltraElement,
                        Passives = magicite.Passives.Select(y => new DbMagicitePassives
                        {
                            PassiveName = y.Key,
                            PassiveValue = y.Value
                        }).ToList()
                    });
                }
            }

            dbContext.UpdateRange(dbMagicites);
            dbContext.SaveChanges();
        }
    }
}
