using Microsoft.EntityFrameworkCore;
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

        public IList<DbSoulbreaks> GetSoulbreaksByCharacters(string charName)
        {
            var soulbreaks = dbContext.Soulbreaks
                .Include(x => x.BraveCommands)
                .Include(x => x.BurstCommands)
                .Include(x => x.SynchroCommands)
                .Include(x => x.SoulbreakStatuses)
                .ThenInclude(x => x.Status)
                .Where(x => x.Character.ToUpper() == charName.ToUpper())
                .ToList();

            return soulbreaks;
        }
        public void UpdateSynchroCommandsFromSheet(IList<SheetSynchros> sheetSynchros)
        {
            foreach(var synchro in sheetSynchros)
            {
                var currentSynchro = dbContext.SynchroCommands.FirstOrDefault(x => x.SynchroId == synchro.SynchroId);

                if(currentSynchro == null)
                {
                    currentSynchro = new DbSynchroCommands();
                }

                currentSynchro.SynchroId = synchro.SynchroId;
                currentSynchro.SynchroSlot = synchro.SynchroSlot;
                currentSynchro.SynchroConditionId = synchro.SynchroConditionId;
                currentSynchro.SynchroCondition = synchro.SynchroCondition;
                currentSynchro.Source = synchro.Source;
                currentSynchro.School = synchro.School;
                currentSynchro.SB = synchro.SB;
                currentSynchro.Name = synchro.Name;
                currentSynchro.Character = synchro.Character;
                currentSynchro.Effects = synchro.Effects;
                currentSynchro.Element = synchro.Element;
                currentSynchro.Formula = synchro.Formula;
                currentSynchro.JPName = synchro.JPName;
                currentSynchro.Target = synchro.Target;
                currentSynchro.Time = synchro.Time;
                currentSynchro.Type = synchro.Type;

                if(currentSynchro.Id == 0)
                {
                    dbContext.Add(currentSynchro);
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateSoulbreakStatuses()
        {
            var soulbreaks = dbContext.Soulbreaks.Include(x => x.SoulbreakStatuses).ToList();
            var statuses = dbContext.Statuses.ToList();

            foreach(var soulbreak in soulbreaks)
            {
                var matchedStatuses = statuses.Where(x => soulbreak.Effects.Contains(x.Name)).ToList();

                var uniqueStatuses = new List<DbStatuses>();

                var orderedmatches = matchedStatuses.OrderByDescending(x => x.Name.Length).ToList();

                foreach(var match in orderedmatches.Where(x => x.Effects != ""))
                {
                    if(uniqueStatuses.Where(x => x.Name.Contains(match.Name)).Count() == 0)
                    {
                        uniqueStatuses.Add(match);
                    }
                }

                foreach(var status in uniqueStatuses)
                {
                    if(soulbreak.SoulbreakStatuses == null)
                    {
                        soulbreak.SoulbreakStatuses = new List<DbSoulbreakStatuses>();
                    }
                    
                    var matchedStatus = soulbreak.SoulbreakStatuses.FirstOrDefault(x => x.StatusId == status.Id);

                    if(matchedStatus == null)
                    {
                        matchedStatus = new DbSoulbreakStatuses();
                        soulbreak.SoulbreakStatuses.Add(matchedStatus);
                    }
                    matchedStatus.StatusId = status.Id;
                    matchedStatus.Status = status;
                    matchedStatus.Soulbreak = soulbreak;
                    matchedStatus.SoulbreakId = soulbreak.Id;
                }


            }

            dbContext.SaveChanges();
        }
        public void UpdateBurstCommandsFromSheet(IList<SheetBursts> sheetBursts)
        {
            foreach (var burst in sheetBursts)
            {
                var currentBurst = dbContext.BurstCommands.FirstOrDefault(x => x.BurstId == burst.BurstId);

                if (currentBurst == null)
                {
                    currentBurst = new DbBurstCommands();
                }

                currentBurst.BurstId = burst.BurstId;
                currentBurst.Source = burst.Source;
                currentBurst.School = burst.School;
                currentBurst.SB = burst.SB;
                currentBurst.Name = burst.Name;
                currentBurst.Character = burst.Character;
                currentBurst.Effects = burst.Effects;
                currentBurst.Element = burst.Element;
                currentBurst.Formula = burst.Formula;
                currentBurst.JPName = burst.JPName;
                currentBurst.Target = burst.Target;
                currentBurst.Time = burst.Time;
                currentBurst.Type = burst.Type;

                if (currentBurst.Id == 0)
                {
                    dbContext.Add(currentBurst);
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateBraveCommandsFromSheet(IList<SheetBraves> sheetBraves)
        {
            foreach (var brave in sheetBraves)
            {
                var currentBrave = dbContext.BraveCommands.FirstOrDefault(x => x.BraveId == brave.BraveId);

                if (currentBrave == null)
                {
                    currentBrave = new DbBraveCommands();
                }

                currentBrave.BraveId = brave.BraveId;
                currentBrave.BraveCondition = brave.BraveCondition;
                currentBrave.BraveLevel = brave.BraveLevel;
                currentBrave.Source = brave.Source;
                currentBrave.School = brave.School;
                currentBrave.SB = brave.SB;
                currentBrave.Name = brave.Name;
                currentBrave.Character = brave.Character;
                currentBrave.Effects = brave.Effects;
                currentBrave.Element = brave.Element;
                currentBrave.Formula = brave.Formula;
                currentBrave.JPName = brave.JPName;
                currentBrave.Target = brave.Target;
                currentBrave.Time = brave.Time;
                currentBrave.Type = brave.Type;

                if (currentBrave.Id == 0)
                {
                    dbContext.Add(currentBrave);
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateRecordMateriaFromSheet(IList<SheetRecordMaterias> recordMaterias)
        {
            foreach(var materia in recordMaterias)
            {
                var currentMateria = dbContext.RecordMaterias.FirstOrDefault(x => x.RMId == materia.RMId);
                
                if(currentMateria == null)
                {
                    currentMateria = new DbRecordMaterias();
                }

                currentMateria.RMId = materia.RMId;
                currentMateria.Realm = materia.Realm;
                currentMateria.Name = materia.Name;
                currentMateria.JPName = materia.JPName;
                currentMateria.Effect = materia.Effect;
                currentMateria.Character = materia.Character;
                currentMateria.UnlockCriteria = materia.UnlockCriteria;
                
                if(currentMateria.Id == 0)
                {
                    dbContext.Add(currentMateria);
                }
                else
                {
                    dbContext.Update(currentMateria);
                }

            }
        }

        public void UpdateLegendMateriaFromSheet(IList<SheetLegendMaterias> legendMaterias)
        {
            foreach (var materia in legendMaterias)
            {
                var currentMateria = dbContext.LegendMaterias.FirstOrDefault(x => x.LMId == materia.LMId);

                if (currentMateria == null)
                {
                    currentMateria = new DbLegendMaterias();
                }

                currentMateria.LMId = materia.LMId;
                currentMateria.Realm = materia.Realm;
                currentMateria.Name = materia.Name;
                currentMateria.JPName = materia.JPName;
                currentMateria.Effect = materia.Effect;
                currentMateria.Character = materia.Character;
                currentMateria.Master = materia.Master;
                currentMateria.Relic = materia.Relic;
                currentMateria.Anima = materia.Anima;

                if (currentMateria.Id == 0)
                {
                    dbContext.Add(currentMateria);
                }
                else
                {
                    dbContext.Update(currentMateria);
                }

            }
        }
        public void UpdateSoulbreaksFromSheet(IList<SheetSoulbreaks> soulbreaks, IList<string> charNames)
        {
            foreach (var soulbreak in soulbreaks.Where(x => x.Tier != "RW" && charNames.Contains(x.Character)))
            {
                var soulbreakToUpdate = dbContext.Soulbreaks.FirstOrDefault(x => x.SoulbreakId == soulbreak.SoulbreakId);
                if(soulbreakToUpdate == null)
                {
                    soulbreakToUpdate = new DbSoulbreaks();
                    soulbreakToUpdate.Name = soulbreak.Name;
                    soulbreakToUpdate.Character = soulbreak.Character;
                }
                else
                {
                    soulbreakToUpdate.Anima = soulbreak.Anima;
                    soulbreakToUpdate.Effects = soulbreak.Effects;
                    soulbreakToUpdate.Element = soulbreak.Element;
                    soulbreakToUpdate.Formula = soulbreak.Formula;
                    soulbreakToUpdate.JPName = soulbreak.JPName;
                    soulbreakToUpdate.Multiplier = soulbreak.Multiplier;
                    soulbreakToUpdate.Points = soulbreak.Points;
                    soulbreakToUpdate.Realm = soulbreak.Realm;
                    soulbreakToUpdate.Relic = soulbreak.Relic;
                    soulbreakToUpdate.SoulbreakBonus = soulbreak.SoulbreakBonus;
                    soulbreakToUpdate.SoulbreakId = soulbreak.SoulbreakId;
                    soulbreakToUpdate.Target = soulbreak.Target;
                    soulbreakToUpdate.Tier = soulbreak.Tier;
                    soulbreakToUpdate.Type = soulbreak.Type;
                    soulbreakToUpdate.Time = soulbreak.Time;

                    if(soulbreakToUpdate.Id == 0)
                    {
                        dbContext.Add(soulbreakToUpdate);
                    }
                }
            }
            dbContext.SaveChanges();
        }
    }
}
