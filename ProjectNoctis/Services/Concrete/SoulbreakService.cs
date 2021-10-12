using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class SoulbreakService : ISoulbreakService
    {
        public ISoulbreakRepository soulbreakRepository;
        public IStatusRepository statusRepository;

        public SoulbreakService(ISoulbreakRepository soulbreakRepository, IStatusRepository statusRepository)
        {
            this.soulbreakRepository = soulbreakRepository;
            this.statusRepository = statusRepository;
        }

        public List<Soulbreak> BuildSoulbreakInfoFromCharNameAndTier(string tier, string character, int? index = null)
        {
            var soulbreaksFromDb = soulbreakRepository.GetSoulbreaksByCharacterName(tier, character, index);
            var newSoulbreaks = new List<Soulbreak>();

            foreach (var soulbreak in soulbreaksFromDb)
            {
                var newSoulbreak = new Soulbreak();
                newSoulbreak.SoulbreakStatuses = statusRepository.GetStatusesByEffectText(soulbreak.Name, soulbreak.Effects);
                FilterAttachElementFromStatuses(newSoulbreak.SoulbreakStatuses);
                newSoulbreak.SoulbreakOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newSoulbreak.SoulbreakStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newSoulbreak.SoulbreakOthers);
                }

                newSoulbreak.Info = soulbreak;


                foreach(var other in newSoulbreak.SoulbreakOthers)
                {
                    foreach(var otherStatus in other.Value)
                    {
                        FilterAttachElementFromStatuses(otherStatus.OtherStatuses);
                    }
                }

                AddSynchroCommandsToSoulbreak(newSoulbreak);
                AddBurstCommandsToSoulbreak(newSoulbreak);
                AddBraveCommandsToSoulbreak(newSoulbreak);

                newSoulbreaks.Add(newSoulbreak);

            };

            return newSoulbreaks;
        }

        public void FilterAttachElementFromStatuses(Dictionary<string, List<SheetStatus>> statuses)
        {
            SheetStatus attachElementTemplate = new SheetStatus() { DefaultDuration = "25", Effects = "Replaces Attack command, increases {Element} damage dealt by 50/80/120% (abilities) or 80/100/120% (Soul Breaks), {Element} resistance +20%" };
            SheetStatus attachElementStackingTemplate = new SheetStatus() { DefaultDuration = "25", Effects = "Allow to stack Attach {Element}, up to Attach {Element} 3" };
            SheetStatus buffElementTemplate = new SheetStatus() { DefaultDuration = "15", Effects = "Increases {Element} damage dealt by 10%, cumulable" };
            SheetStatus imperilElementTemplate = new SheetStatus() { DefaultDuration = "15", Effects = "{Element} Resistance -10%, cumulable" };

            var attachElements = statuses
                    .Select(x => new KeyValuePair<string, List<SheetStatus>>
                        (x.Key,
                            x.Value.Where(y => y.Name.Contains("Attach")
                            && !y.Name.Contains("Stacking"))
                            .ToList()))
                    .Where(x => x.Value.Count() != 0 && x.Value.Count() != 1)
                    .ToList();

            var stackingElements = statuses.Select(x => new KeyValuePair<string, List<SheetStatus>>
                    (x.Key,
                    x.Value.Where(y => y.Name.Contains("Attach")
                    && y.Name.Contains("Stacking"))
                    .ToList()))
                .Where(x => x.Value.Count() != 0 && x.Value.Count() != 1)
                .ToList();

            var buffElements = statuses.Select(x => new KeyValuePair<string, List<SheetStatus>>
                    (x.Key,
                    x.Value.Where(y => y.Name.Contains("Buff"))
                    .ToList()))
                .Where(x => x.Value.Count() != 0 && x.Value.Count() != 1)
                .ToList();

            var imperilElements = statuses.Select(x => new KeyValuePair<string, List<SheetStatus>>
                    (x.Key,
                    x.Value.Where(y => y.Name.Contains("Imperil"))
                    .ToList()))
                .Where(x => x.Value.Count() != 0 && x.Value.Count() != 1)
                .ToList();

            if (attachElements.Count() == 0 && stackingElements.Count() == 0 && buffElements.Count() == 0 && imperilElements.Count() == 0)
            {
                return;
            }


            //Add Attached Elements
            AddElements(statuses, attachElementTemplate, attachElements, "Attach {Element}");

            //Add Stacking Attached Elements
            AddElements(statuses, attachElementStackingTemplate, stackingElements, "Attach {Element} With Stacking");

            //Add Buffed Elements
            AddElements(statuses, buffElementTemplate, buffElements, "Buff {Element}");

            //Add Impleril Elements
            AddElements(statuses, imperilElementTemplate, imperilElements, "Imperil {Element}");

        }

        private static void AddElements(Dictionary<string, List<SheetStatus>> statuses, SheetStatus templateStatus, List<KeyValuePair<string, List<SheetStatus>>> elementalStatuses, string nameTemplate)
        {
            if (elementalStatuses.Count() == 0)
                return;
                
            var elements = new List<string>();
            foreach (var source in elementalStatuses)
            {
                foreach (var element in source.Value)
                {
                    statuses[source.Key].Remove(element);
                    elements.AddRange(Constants.Constants.elementList.Where(x => element.Name.Contains(x)).Distinct());
                }
            }
            elements = elements.Distinct().ToList<string>();

            var omni = templateStatus;
            omni.Name = nameTemplate.Replace("{Element}", string.Join(", ", elements));
            omni.Effects = omni.Effects.Replace("{Element}", string.Join(", ", elements));
            statuses[statuses.First().Key].Add(omni);
        }

        public List<LimitBreak> BuildLimitInfoFromCharNameAndTier(string tier, string character, int? index = null)
        {
            var limitsFromDb = soulbreakRepository.GetLimitBreaksByCharacterNameAndTier(tier, character, index);
            var newlimits = new List<LimitBreak>();

            foreach (var limit in limitsFromDb)
            {
                var newlimit = new LimitBreak();
                newlimit.LimitStatuses = statusRepository.GetStatusesByEffectText(limit.Name, limit.Effects);
                FilterAttachElementFromStatuses(newlimit.LimitStatuses);
                newlimit.LimitOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newlimit.LimitStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newlimit.LimitOthers);
                }

                newlimit.Info = limit;

                AddGuardianCommandsToLimitBreak(newlimit);

                newlimits.Add(newlimit);
            };
            return newlimits;
        }

        public List<Soulbreak> BuildSoulbreakInfoForAllCharSoulbreaksFromName(string name)
        {
            var soulbreaksFromDb = soulbreakRepository.GetAllSoulbreaksByCharacterName(name);

            return soulbreaksFromDb.Select(x => new Soulbreak() { Info = x }).ToList();
        }

        public List<Soulbreak> BuildSoulbreakInfoForAnimaWave(string wave)
        {
            var soulbreaksFromDb = soulbreakRepository.GetSoulbreaksByAnimaWave(wave);

            return soulbreaksFromDb.Select(x => new Soulbreak() { Info = x }).ToList();
        }


        public void AddGuardianCommandsToLimitBreak(LimitBreak limitBreak)
        {
            var Guardians = soulbreakRepository.GetGuardianCommandsByCharacterAndLimitBreak(limitBreak.Info.Character, limitBreak.Info.Name);

            limitBreak.GuardianCommands = new List<GuardianCommand>();

            foreach (var guardian in Guardians)
            {
                var newGuardian = new GuardianCommand();

                newGuardian.Info = guardian;

                newGuardian.GuardianStatuses = statusRepository.GetStatusesByEffectText(guardian.Name, guardian.Effects);
                FilterAttachElementFromStatuses(newGuardian.GuardianStatuses);

                newGuardian.GuardianOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newGuardian.GuardianStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newGuardian.GuardianOthers);
                }

                foreach (var other in newGuardian.GuardianOthers)
                {
                    foreach (var otherStatus in other.Value)
                    {
                        FilterAttachElementFromStatuses(otherStatus.OtherStatuses);
                    }
                }

                limitBreak.GuardianCommands.Add(newGuardian);
            }
        }

        public void AddSynchroCommandsToSoulbreak(Soulbreak soulbreak)
        {
            var Synchros = soulbreakRepository.GetSynchrosByCharacterAndSoulbreak(soulbreak.Info.Character, soulbreak.Info.Name);

            soulbreak.SynchroCommands = new List<SynchroCommand>();

            foreach (var synchro in Synchros)
            {
                var newSynchro = new SynchroCommand();

                newSynchro.Info = synchro;

                newSynchro.SynchroStatuses = statusRepository.GetStatusesByEffectText(synchro.Name, synchro.Effects);
                FilterAttachElementFromStatuses(newSynchro.SynchroStatuses);

                newSynchro.SynchroOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newSynchro.SynchroStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newSynchro.SynchroOthers);
                }

                foreach (var other in newSynchro.SynchroOthers)
                {
                    foreach (var otherStatus in other.Value)
                    {
                        FilterAttachElementFromStatuses(otherStatus.OtherStatuses);
                    }
                }

                soulbreak.SynchroCommands.Add(newSynchro);
            }
        }

        public void AddBurstCommandsToSoulbreak(Soulbreak soulbreak)
        {
            var bursts = soulbreakRepository.GetBurstsByCharacterAndSoulbreak(soulbreak.Info.Character, soulbreak.Info.Name);

            soulbreak.BurstCommands = new List<BurstCommand>();

            foreach (var burst in bursts)
            {
                var newBurst = new BurstCommand();

                newBurst.Info = burst;
                newBurst.BurstStatuses = statusRepository.GetStatusesByEffectText(burst.Name, burst.Effects);
                FilterAttachElementFromStatuses(newBurst.BurstStatuses);
                newBurst.BurstOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newBurst.BurstStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newBurst.BurstOthers);
                }

                foreach (var other in newBurst.BurstOthers)
                {
                    foreach (var otherStatus in other.Value)
                    {
                        FilterAttachElementFromStatuses(otherStatus.OtherStatuses);
                    }
                }

                soulbreak.BurstCommands.Add(newBurst);
            }
        }

        public void AddBraveCommandsToSoulbreak(Soulbreak soulbreak)
        {
            var braves = soulbreakRepository.GetBravesByCharacterAndSoulbreak(soulbreak.Info.Character, soulbreak.Info.Name);

            soulbreak.BraveCommands = new List<BraveCommand>();

            foreach (var brave in braves)
            {
                var newBrave = new BraveCommand();

                newBrave.Info = brave;
                newBrave.BraveStatuses = statusRepository.GetStatusesByEffectText(brave.Name, brave.Effects);
                FilterAttachElementFromStatuses(newBrave.BraveStatuses);
                newBrave.BraveOthers = new Dictionary<string, List<SheetOthers>>();

                foreach (var status in newBrave.BraveStatuses.SelectMany(x => x.Value))
                {
                    statusRepository.GetOthersByNamesAndSource(status.Name, newBrave.BraveOthers);
                }

                foreach (var other in newBrave.BraveOthers)
                {
                    foreach (var otherStatus in other.Value)
                    {
                        FilterAttachElementFromStatuses(otherStatus.OtherStatuses);
                    }
                }

                soulbreak.BraveCommands.Add(newBrave);
            }
        }
    }
}
