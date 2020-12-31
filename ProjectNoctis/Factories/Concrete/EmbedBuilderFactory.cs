using Discord;
using Google.Apis.Util;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Factories.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ProjectNoctis.Factories.Concrete
{
    public class EmbedBuilderFactory : IEmbedBuilderFactory
    {

        private readonly ICharacterService characterService;
        private readonly IAbilityService abilityService;
        private readonly ISoulbreakService soulbreakService;
        private readonly IDiveService diveService;
        private readonly IMagiciteService magiciteService;
        private readonly IStatusService statusService;
        private readonly IMateriaService materiaService;
        private readonly Settings settings;

        public EmbedBuilderFactory
            (
                ICharacterService characterService, 
                IAbilityService abilityService,
                ISoulbreakService soulbreakService,
                IDiveService diveService,
                IMagiciteService magiciteService,
                IStatusService statusService,
                IMateriaService materiaService,
                Settings settings
            )
        {
            this.characterService = characterService;
            this.abilityService = abilityService;
            this.soulbreakService = soulbreakService;
            this.diveService = diveService;
            this.magiciteService = magiciteService;
            this.statusService = statusService;
            this.materiaService = materiaService;
            this.settings = settings;
        }

        public Embed BuildEmbedForBasicCharacterInfo(string name)
        {

            var character = characterService.BuildBasicCharacterInfoByName(name);

            var abilities = character.Info.Skills.OrderByDescending(x => x.Value).GroupBy(x => x.Value);

            var abilityList = abilities.Select(x => new KeyValuePair<int, string>(x.Key, $"{string.Join(",  ", x.Select(y => y.Key))}"));

            var diveAbilities = character.DiveAbilities.Select(x => x.Replace(x.Substring(x.IndexOf("★") - 1, 6), String.Empty));

            var nightMareDives = new List<string>();

            foreach (var abil in Constants.Constants.nightmareEnhancedSkills)
            {
                if (diveAbilities.Contains($"{abil} 5★"))
                {
                    nightMareDives.Add($"{abil} 6★");
                }
            }

            var embed = new EmbedBuilder();

            embed.Title = $"{character.Info.Name}'s Usable Abilities/Equipment";


            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Hp", Value = character.Info.BaseHp, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Atk", Value = character.Info.BaseAtk, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Mag", Value = character.Info.BaseMag, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Acc", Value = character.Info.BaseAcc, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Def", Value = character.Info.BaseDef, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Spd", Value = character.Info.BaseSpd, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Eva", Value = character.Info.BaseEva, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Mnd ", Value = character.Info.BaseMnd, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Base Res", Value = character.Info.BaseRes, IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Weapons", Value = string.Join(",  ", character.Info.Equipment.Where(x => Constants.Constants.weaponList.Contains(x))), IsInline = true });

            embed.Fields.Add(new EmbedFieldBuilder()
            { Name = "Armor", Value = string.Join(",  ", character.Info.Equipment.Where(x => Constants.Constants.armorList.Contains(x))), IsInline = true });

            embed.AddField(new EmbedFieldBuilder() { Name = "\u200B", Value = "\u200B", IsInline = true });

            foreach (var abilityLevel in abilityList)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                { Name = $"{abilityLevel.Key}★ Ability Access", Value = abilityLevel.Value, IsInline = true });
            }

            if (diveAbilities.Count() != 0)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                { Name = "Abilities Gained From Dive", Value = string.Join(",  ", diveAbilities) });
            }

            if (nightMareDives.Count() != 0)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                { Name = "Abilities Gained From Nightmare + Dive", Value = string.Join(",  ", nightMareDives) });
            }

            embed.ThumbnailUrl = character.Info.CharacterImage;

            return embed.Build();

        }

        public Embed BuildEmbedForAbilityBySchoolInformation(string school, string rank, string element)
        {
            var embed = new EmbedBuilder();

            var abilities = abilityService.BuildAbilityBySchoolInfo(school, rank, element);

            if (abilities.Count() == 0)
            {
                embed.Title = "No Matches Found";
                return embed.Build();
            }

            element = element != null ? abilities.Select(x => x.Info.Element).FirstOrDefault() : null;

            if (element == "-")
            {
                element = "Non-Elemental";
            }

            embed.Title = $"{abilities.Select(x => x.Info.School).FirstOrDefault()} {rank}{" " + element + " "}Ability List";
            var abilityGroups = abilities.GroupBy(x => x.Info.Element);

            var abilityList = abilityGroups.Select(x => new KeyValuePair<string, string>(x.Key, $"{string.Join(",  ", x.Select(y => y.Info.Name.Contains("Only") ? y.Info.Name : $"**{y.Info.Name}**"))}"));
            var abilityDict = new Dictionary<string, string>();

            foreach (var abil in abilityList)
            {
                abilityDict.Add(abil.Key, abil.Value);
            }

            var abilDictCopy = new Dictionary<string, string>(abilityDict);

            foreach (var abilElement in abilDictCopy)
            {
                if (abilElement.Key.Contains(","))
                {
                    var eleList = abilElement.Key.Split(',').Select(x => x.Trim()).ToList();

                    foreach (var ele in eleList)
                    {
                        if (abilityDict.ContainsKey(ele))
                        {
                            abilityDict[ele] += $", {abilElement.Value}";
                        }
                        else
                        {
                            if (ele == "NE")
                            {
                                if (abilityDict.ContainsKey("-"))
                                {
                                    abilityDict["-"] += $", {abilElement.Value}";
                                }
                                else
                                {
                                    abilityDict.Add("-", abilElement.Value);
                                }
                            }
                            else
                            {
                                abilityDict.Add(ele, abilElement.Value);
                            }
                        }
                    }
                }
            }

            abilityDict.Keys.Where(x => x.Contains(',')).ToList().ForEach(x => abilityDict.Remove(x));

            foreach (var ability in abilityDict)
            {
                embed.AddField(new EmbedFieldBuilder()
                {
                    Name = ability.Key != "-" && ability.Key != string.Empty ? ability.Key : "Non-Elemental",
                    Value = ability.Value
                });
            }

            return embed.Build();
        }

        public List<Embed> BuildEmbedForAbilityInformation(string name, bool heroAbility)
        {
            var abilities = heroAbility ? abilityService.BuildHeroAbilityInfo(name) : new List<Ability>() { abilityService.BuildAbilityInfo(name) };
            var embeds = new List<Embed>();

            if (abilities == null)
            {
                var embed = new EmbedBuilder();
                embed.Title = "No Matches Found";
                embeds.Add(embed.Build());
                return embeds;
            }

            foreach (var ability in abilities)
            {
                var embed = new EmbedBuilder();

                if (ability == null)
                {
                    embed.Title = "No Matches Found";
                    embeds.Add(embed.Build());
                    return embeds;
                }

                embed.Title = ability.Info.Name;
                embed.ThumbnailUrl = ability.Info.AbilityImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effect",
                    Value = ability.Info.Effects != string.Empty ? ability.Info.Effects : "-"
                });

                var statusFields = BuildStatusEmbedFields(ability.AbilityStatuses);

                embed.Fields.AddRange(statusFields);

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = ability.Info.Type != string.Empty ? ability.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = ability.Info.Target != string.Empty ? ability.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = ability.Info.Multiplier != string.Empty ? ability.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = ability.Info.Time != string.Empty ? ability.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Charge",
                    Value = ability.Info.SB != string.Empty ? ability.Info.SB : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "School",
                    Value = ability.Info.School != string.Empty ? ability.Info.School : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = ability.Info.Element != string.Empty ? ability.Info.Element : "-",
                    IsInline = true
                });

                var orbs = new List<string>();

                foreach (var orb in ability.Info.OrbsRequired.Keys)
                {
                    var orbReq = $"**{ability.Info.OrbsRequired[orb]}: **";
                    var total = 0;

                    for (int i = 1; i <= 5; i++)
                    {
                        if (ability.Info.OrbCosts.ContainsKey($"{orb}-R{i}"))
                        {
                            var orbCost = ability.Info.OrbCosts[$"{orb}-R{i}"];

                            if (orbCost == "-")
                            {
                                continue;
                            }

                            total += Int32.Parse(orbCost);
                            orbReq += $"| {orbCost} ";
                        }
                        else
                        {
                            break;
                        }
                    }

                    orbReq += $"| ({total})";

                    orbs.Add(orbReq);
                }

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Orbs Required",
                    Value = string.Join("\n", orbs),
                    IsInline = false
                });

                embeds.Add(embed.Build());
            }

            return embeds;
        }

        public List<List<Embed>> BuildSoulbreakEmbeds(string tier, string charName, int? index)
        {
            if (index != null)
            {
                index = index - 1;
            }

            var soulbreaks = soulbreakService.BuildSoulbreakInfoFromCharNameAndTier(tier, charName, index);
            var embeds = new List<List<Embed>>();

            if (soulbreaks.Count() == 0)
            {
                var embedGroupTemp = new List<Embed>();
                var tempEmbed = new EmbedBuilder();
                tempEmbed.Title = "No Matches Found";

                embedGroupTemp.Add(tempEmbed.Build());
                embeds.Add(embedGroupTemp);
            }

            foreach (var soulbreak in soulbreaks)
            {
                var embedGroup = new List<Embed>();
                var embed = new EmbedBuilder();

                embed.Title = $"{soulbreak.Info.Character}: {soulbreak.Info.Name}  {{{soulbreak.Info.Relic}}}";
                embed.ThumbnailUrl = soulbreak.Info.SoulbreakImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = soulbreak.Info.Effects != string.Empty ? soulbreak.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = soulbreak.Info.Element != string.Empty ? soulbreak.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = soulbreak.Info.Type != string.Empty ? soulbreak.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = soulbreak.Info.Target != string.Empty ? soulbreak.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = soulbreak.Info.Multiplier != string.Empty ? soulbreak.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = soulbreak.Info.Time != string.Empty ? soulbreak.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Type",
                    Value = soulbreak.Info.Tier != string.Empty ? soulbreak.Info.Tier : "-",
                    IsInline = true
                });

                
                if (soulbreak.Info.Anima != null)
                {
                    var animaValue = $"Yes (Wave: {soulbreak.Info.Anima})";

                    if(soulbreak.Info.Anima == settings.JpAnimaWave)
                    {
                        animaValue = $"JP Only (Wave: {soulbreak.Info.Anima})";
                    }

                    if(soulbreak.Info.Anima == "")
                    {
                        animaValue = "No";
                    }

                    embed.Fields.Add(new EmbedFieldBuilder()
                    {
                        Name = "Anima Available",
                        Value = animaValue,
                        IsInline = true
                    });
                }

                embed.Fields.AddRange(BuildStatusEmbedFields(soulbreak.SoulbreakStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(soulbreak.SoulbreakOthers));

                if (soulbreak.BraveCommands.Count() != 0 && tier != "brave")
                {
                    embed.AddField(new EmbedFieldBuilder()
                    {
                        Name = "Commands",
                        Value = $"**Please use ?brave to see commands and use index if there are multiple braves for that character (e.g: ?brave {soulbreak.Info.Character} 1)**",
                        IsInline = false
                    });
                }

                if (soulbreak.BurstCommands.Count() != 0 && (index == null || index < 0 || index >= soulbreaks.Count()) && soulbreaks.Count() > 1)
                {
                    embed.AddField(new EmbedFieldBuilder()
                    {
                        Name = "Commands",
                        Value = $"**Please use ?bsb {soulbreak.Info.Character} {soulbreaks.IndexOf(soulbreak) + 1} to see commands for this particular bsb**",
                        IsInline = false
                    });
                }

                if (soulbreak.BraveCommands.Count() != 0 && (index == null || index < 0 || index >= soulbreaks.Count()) && soulbreaks.Count() > 1 && tier == "brave")
                {
                    embed.AddField(new EmbedFieldBuilder()
                    {
                        Name = "Commands",
                        Value = $"**Please use ?brave {soulbreak.Info.Character} {soulbreaks.IndexOf(soulbreak) + 1} to see commands for this particular brave**",
                        IsInline = false
                    });
                }

                if (soulbreak.SynchroCommands.Count() != 0 && (index == null || index < 0 || index >= soulbreaks.Count()) && soulbreaks.Count() > 1)
                {
                    embed.AddField(new EmbedFieldBuilder()
                    {
                        Name = "Commands",
                        Value = $"**Please use ?sasb {soulbreak.Info.Character} {soulbreaks.IndexOf(soulbreak) + 1} to see commands for this particular synchro**",
                        IsInline = false
                    });
                }

                embedGroup.Add(embed.Build());

                if (soulbreak.BraveCommands.Count() != 0 && tier == "brave" && (soulbreaks.Count() == 1 || (index == null && index > -1 && index < soulbreaks.Count())))
                {
                    embedGroup.AddRange(BuildBraveCommandEmbeds(soulbreak.BraveCommands));
                }

                if (soulbreak.BurstCommands.Count() != 0 && (soulbreaks.Count() == 1 || (index == null && index > -1 && index < soulbreaks.Count())))
                {
                    embedGroup.AddRange(BuildBurstCommandEmbeds(soulbreak.BurstCommands));
                }

                if (soulbreak.SynchroCommands.Count() != 0 && (soulbreaks.Count() == 1 || (index == null && index > -1 && index < soulbreaks.Count())))
                {
                    embedGroup.AddRange(BuildSynchroCommandEmbeds(soulbreak.SynchroCommands));
                }

                embeds.Add(embedGroup);
            }

            return embeds;
        }

        public List<Embed> BuildLimitBreakEmbeds(string tier, string charName, int? index)
        {
            if (index != null)
            {
                index = index - 1;
            }

            var limits = soulbreakService.BuildLimitInfoFromCharNameAndTier(tier, charName, index);
            var embeds = new List<Embed>();

            if (limits.Count() == 0)
            {
                var tempEmbed = new EmbedBuilder();
                tempEmbed.Title = "No Matches Found";

                embeds.Add(tempEmbed.Build());
                return embeds;
            }

            foreach (var limit in limits)
            {
                var embed = new EmbedBuilder();

                embed.Title = $"{limit.Info.Character}: {limit.Info.Name}  {{{limit.Info.Relic}}}";
                embed.ThumbnailUrl = limit.Info.LimitBreakImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = limit.Info.Effects != string.Empty ? limit.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = limit.Info.Element != string.Empty ? limit.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = limit.Info.Type != string.Empty ? limit.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = limit.Info.Target != string.Empty ? limit.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = limit.Info.Multiplier != string.Empty ? limit.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Honing Effects",
                    Value = limit.Info.LimitBonus != string.Empty ? limit.Info.LimitBonus : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = limit.Info.Time != string.Empty ? limit.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Type",
                    Value = limit.Info.Tier != string.Empty ? limit.Info.Tier : "-",
                    IsInline = true
                });

                embed.Fields.AddRange(BuildStatusEmbedFields(limit.LimitStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(limit.LimitOthers));
                embeds.Add(embed.Build());
                embeds.AddRange(BuildGuardianCommandEmbeds(limit.GuardianCommands));
            }

            return embeds;
        }

        public List<Embed> BuildBraveCommandEmbeds(IList<BraveCommand> braveCommands)
        {
            var braveEmbeds = new List<Embed>();

            foreach (var command in braveCommands)
            {
                var embed = new EmbedBuilder();

                embed.Title = $"{command.Info.Source}'s {command.Info.Name} Level {command.Info.BraveLevel}";
                embed.ThumbnailUrl = command.Info.BraveImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = command.Info.Effects != string.Empty ? command.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Levels Up On",
                    Value = command.Info.BraveCondition != string.Empty ? command.Info.BraveCondition : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "School",
                    Value = command.Info.School != string.Empty ? command.Info.School : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = command.Info.Element != string.Empty ? command.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = command.Info.Type != string.Empty ? command.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = command.Info.Multiplier != string.Empty ? command.Info.Multiplier: "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = command.Info.Target != string.Empty ? command.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = command.Info.Time != string.Empty ? command.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Charge",
                    Value = command.Info.SB != string.Empty ? command.Info.SB : "-",
                    IsInline = true
                });

                embed.Fields.AddRange(BuildStatusEmbedFields(command.BraveStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(command.BraveOthers));

                braveEmbeds.Add(embed.Build());
            }

            return braveEmbeds;
        }

        public List<Embed> BuildBurstCommandEmbeds(IList<BurstCommand> burstCommands)
        {
            var burstEmbeds = new List<Embed>();

            foreach (var command in burstCommands)
            {
                var embed = new EmbedBuilder();

                embed.Title = $"{command.Info.Source}'s {command.Info.Name}";
                embed.ThumbnailUrl = command.Info.BurstImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = command.Info.Effects != string.Empty ? command.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "School",
                    Value = command.Info.School != string.Empty ? command.Info.School : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = command.Info.Element != string.Empty ? command.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = command.Info.Type != string.Empty ? command.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = command.Info.Multiplier != string.Empty ? command.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = command.Info.Target != string.Empty ? command.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = command.Info.Time != string.Empty ? command.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Charge",
                    Value = command.Info.SB != string.Empty ? command.Info.SB : "-",
                    IsInline = true
                });

                embed.Fields.AddRange(BuildStatusEmbedFields(command.BurstStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(command.BurstOthers));

                burstEmbeds.Add(embed.Build());
            }

            return burstEmbeds;
        }

        public List<Embed> BuildSynchroCommandEmbeds(IList<SynchroCommand> synchroCommands)
        {
            var synchroEmbeds = new List<Embed>();

            foreach (var command in synchroCommands)
            {
                var embed = new EmbedBuilder();

                embed.Title = $"{command.Info.Source}'s {command.Info.Name} ( Slot {command.Info.SynchroSlot} )";
                embed.ThumbnailUrl = command.Info.SynchroImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = command.Info.Effects != string.Empty ? command.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Links On",
                    Value = command.Info.SynchroCondition != string.Empty ? command.Info.SynchroCondition : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "School",
                    Value = command.Info.School != string.Empty ? command.Info.School : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = command.Info.Element != string.Empty ? command.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = command.Info.Type != string.Empty ? command.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = command.Info.Multiplier != string.Empty ? command.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = command.Info.Target != string.Empty ? command.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = command.Info.Time != string.Empty ? command.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Charge",
                    Value = command.Info.SB != string.Empty ? command.Info.SB : "-",
                    IsInline = true
                });

                embed.Fields.AddRange(BuildStatusEmbedFields(command.SynchroStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(command.SynchroOthers));

                synchroEmbeds.Add(embed.Build());
            }

            return synchroEmbeds;
        }

        public List<Embed> BuildGuardianCommandEmbeds(IList<GuardianCommand> guardianCommands)
        {
            var guardianEmbeds = new List<Embed>();

            foreach (var command in guardianCommands)
            {
                var embed = new EmbedBuilder();

                embed.Title = $"{command.Info.Source}'s {command.Info.Name} ( Slot {command.Info.GuardianSlot} )";

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = command.Info.Effects != string.Empty ? command.Info.Effects : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "School",
                    Value = command.Info.School != string.Empty ? command.Info.School : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Element",
                    Value = command.Info.Element != string.Empty ? command.Info.Element : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Type",
                    Value = command.Info.Type != string.Empty ? command.Info.Type : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Multiplier",
                    Value = command.Info.Multiplier != string.Empty ? command.Info.Multiplier : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Target",
                    Value = command.Info.Target != string.Empty ? command.Info.Target : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Cast Time",
                    Value = command.Info.Time != string.Empty ? command.Info.Time : "-",
                    IsInline = true
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "SB Charge",
                    Value = command.Info.SB != string.Empty ? command.Info.SB : "-",
                    IsInline = true
                });

                embed.Fields.AddRange(BuildStatusEmbedFields(command.GuardianStatuses));
                embed.Fields.AddRange(BuildOtherEmbedFields(command.GuardianOthers));

                guardianEmbeds.Add(embed.Build());
            }

            return guardianEmbeds;
        }

        public Embed BuildEmbedForRecordDive(string name)
        {
            var dive = diveService.BuildRecordDiveByCharacter(name);
            var character = characterService.BuildBasicCharacterInfoByName(name);

            var embed = new EmbedBuilder();

            if (dive.RecordDive == null)
            {
                embed.Title = "Character does not appear to have a record dive yet.";
                return embed.Build();
            }

            embed.Title = character.Info.Name;
            embed.ThumbnailUrl = character.Info.CharacterImage;

            var stats = BuildSortedStringFromStats(dive.RecordDive.Stats);
            var misc = string.Join(", ", dive.RecordDive.Misc);

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Stats",
                Value = stats != string.Empty ? stats : "-"
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Misc",
                Value = misc != string.Empty ? misc : "-"
            });

            return embed.Build();
        }

        public Embed BuildEmbedForLegendDive(string name)
        {
            var dive = diveService.BuildLegendDiveByCharacter(name);
            var character = characterService.BuildBasicCharacterInfoByName(name);

            var embed = new EmbedBuilder();

            if (dive.LegendDive == null)
            {
                embed.Title = "Character does not appear to have a legend dive yet.";
                return embed.Build();
            }

            embed.Title = character.Info.Name;
            embed.ThumbnailUrl = character.Info.CharacterImage;

            var stats = BuildSortedStringFromStats(dive.LegendDive.Stats);
            var misc = string.Join(", ", dive.LegendDive.Misc.Where(x => !Constants.Constants.miscFilter.Contains(x)));
            var motesRequired = $"{dive.LegendDive.MoteOne}, {dive.LegendDive.MoteTwo}";

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Stats",
                Value = stats != string.Empty ? stats : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Motes Required",
                Value = motesRequired != string.Empty ? motesRequired : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Misc",
                Value = misc != string.Empty ? misc : "-"
            });

            return embed.Build();
        }

        public Embed BuildEmbedForRecordBoard(string name)
        {
            var dive = diveService.BuildRecordBoardByCharacter(name);
            var character = characterService.BuildBasicCharacterInfoByName(name);

            var embed = new EmbedBuilder();

            if (dive.Board == null)
            {
                embed.Title = "Character does not appear to have a board yet.";
                return embed.Build();
            }

            embed.Title = character.Info.Name;
            embed.ThumbnailUrl = character.Info.CharacterImage;


            var stats = BuildSortedStringFromStats(dive.Board.BoardBonusStats);
            var misc = string.Join(", ", dive.Board.BoardMisc.Where(x => !Constants.Constants.miscFilter.Contains(x)));

            if (dive.Board.MotesRequired.ContainsKey("100"))
            {
                dive.Board.MotesRequired.Add("Record Sapphire", 100);
                dive.Board.MotesRequired.Remove("100");
            }

            var motesRequired = string.Join("\n", dive.Board.MotesRequired.Select(x => $"**{x.Key}:** {x.Value}").ToList());

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Stats",
                Value = stats != string.Empty ? stats : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Motes Required",
                Value = motesRequired != string.Empty ? motesRequired : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Misc",
                Value = misc != string.Empty ? misc : "-"
            });

            return embed.Build();
        }

        public Embed BuildEmbedForFullDive(string name)
        {
            var dive = diveService.BuildFullDiveByCharacter(name);
            var character = characterService.BuildBasicCharacterInfoByName(name);

            var embed = new EmbedBuilder();

            if (dive.Board == null && dive.LegendDive == null && dive.RecordDive == null)
            {
                embed.Title = "Character does not appear to have any dives yet.";
                return embed.Build();
            }

            embed.Title = character.Info.Name;
            embed.ThumbnailUrl = character.Info.CharacterImage;

            var fullDiveStats = new Dictionary<string, int>();
            var fullMisc = new List<string>();


            var miscGroups = new List<List<string>>()
            {
                dive.RecordDive?.Misc
            };

            var diveGroups = new List<Dictionary<string, int>>()
            {
                dive.RecordDive.Stats
            };

            if (dive.LegendDive != null)
            {
                diveGroups.Add(dive.LegendDive.Stats);
                miscGroups.Add(dive.LegendDive.Misc);
            }

            if (dive.Board != null)
            {
                diveGroups.Add(dive.Board.BoardBonusStats);
                miscGroups.Add(dive.Board.BoardMisc);

                if (dive.Board.MotesRequired.ContainsKey("100"))
                {
                    dive.Board.MotesRequired.Add("Record Sapphire", 100);
                    dive.Board.MotesRequired.Remove("100");
                }
            }

            foreach (var statGroup in diveGroups)
            {
                foreach (var stat in statGroup)
                {
                    if (fullDiveStats.ContainsKey(stat.Key))
                    {
                        fullDiveStats[stat.Key] += stat.Value;
                    }
                    else
                    {
                        fullDiveStats.Add(stat.Key, stat.Value);
                    }
                }
            }

            foreach (var miscGroup in miscGroups)
            {
                fullMisc.AddRange(miscGroup);
            }
            string boardMotesRequired = "-";
            string motesRequired = "-";
            var stats = BuildSortedStringFromStats(fullDiveStats);
            if (dive.LegendDive != null)
            {
                motesRequired = $"{dive.LegendDive.MoteOne}, {dive.LegendDive.MoteTwo}";
            }

            if (dive.Board != null)
            {
                boardMotesRequired = string.Join("\n", dive.Board.MotesRequired.Select(x => $"{x.Key}: {x.Value}").ToList());
            }
            var misc = string.Join(", ", fullMisc.Where(x => !Constants.Constants.miscFilter.Contains(x)));

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Stats",
                Value = stats != string.Empty ? stats : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Motes Required",
                Value = $"**Legend Motes:** \n {motesRequired} \n **Board Motes:** \n {boardMotesRequired}",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Misc",
                Value = misc != string.Empty ? misc : "-",
            });

            if (dive.LegendDive == null)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Missing Legend Dive",
                    Value = "This character does not have a legend dive. Dive has been calculated without it."
                });
            }

            if (dive.Board == null)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Missing Record Board",
                    Value = "This character does not have a record board. Dive has been calculated without it."
                });
            }

            return embed.Build();
        }

        public Embed BuildEmbedForMagicite(string name)
        {
            var magiciteMatch = magiciteService.BuildMagiciteInfoFromName(name);

            var embed = new EmbedBuilder();
            embed.Title = $"{magiciteMatch.Info.Name} {magiciteMatch.Info.Rarity}★";
            embed.ThumbnailUrl = magiciteMatch.Info.MagiciteImage;

            var passiveString = string.Join(", ",magiciteMatch.Info.Passives.Select(x => $"{x.Key} {x.Value}"));

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Element",
                Value = magiciteMatch.Info.Element != string.Empty ? magiciteMatch.Info.Element : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Weakness",
                Value = magiciteMatch.Info.Weakness,
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = $"Ultra Skill: {magiciteMatch.Info.MagiciteUltra} ({magiciteMatch.Info.UltraElement})",
                Value = magiciteMatch.Info.Effects != string.Empty ? magiciteMatch.Info.Effects : "-"
            });

            embed.Fields.AddRange(BuildStatusEmbedFields(magiciteMatch.MagiciteStatuses));

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Passives",
                Value = passiveString != string.Empty ? passiveString : "-"
            });

            return embed.Build();
        }

        public List<Embed> BuildEmbedsForCharacterSoulbreaks(string name)
        {
            var soulbreaks = soulbreakService.BuildSoulbreakInfoForAllCharSoulbreaksFromName(name);
            var character = characterService.BuildBasicCharacterInfoByName(name);

            var pageAmount = Math.Ceiling(soulbreaks.Count() / 20.0);

            var embedGroup = new List<Embed>();

            if (soulbreaks.Count() == 0)
            {
                var errorEmbed = new EmbedBuilder();
                errorEmbed.Title = "No Matches Found For Character";

                embedGroup.Add(errorEmbed.Build());

                return embedGroup;
            }

            for (int i = 0; i < pageAmount; i++)
            {
                var soulbreakPage = GetPage(soulbreaks, i, 20);
                var embed = new EmbedBuilder();
                embed.Title = $"{character.Info.Name}'s Soulbreaks (Page {i + 1} of {pageAmount})";
                embed.ThumbnailUrl = character.Info.CharacterImage;

                foreach (var soulbreak in soulbreakPage)
                {
                    embed.Fields.Add(new EmbedFieldBuilder()
                    {
                        Name = $"{soulbreak.Info.Name} - {soulbreak.Info.Tier}",
                        Value = soulbreak.Info.Effects != string.Empty ? soulbreak.Info.Effects : "-"
                    });
                }

                embedGroup.Add(embed.Build());
            }

            return embedGroup;
        }

        public Embed BuildEmbedForStatus(string name)
        {
            var embed = new EmbedBuilder();

            var status = statusService.BuildStatusInfoFromName(name);

            embed.Title = status.Info.Name;
            embed.ThumbnailUrl = "https://cdn.discordapp.com/attachments/717498404040605784/721817184644366346/FFRK_Status.png";

            embed.AddField(new EmbedFieldBuilder()
            {
                Name = "Effects",
                Value = status.Info.Effects != string.Empty ? status.Info.Effects : "-"
            });

            embed.AddField(new EmbedFieldBuilder()
            {
                Name = "Default Duration",
                Value = status.Info.DefaultDuration != string.Empty || status.Info.DefaultDuration != "-" ? $"{status.Info.DefaultDuration}s" : "-",
                IsInline = true
            });

            embed.AddField(new EmbedFieldBuilder()
            {
                Name = "Exclusive Statuses",
                Value = status.Info.ExclusiveStatus != string.Empty ? status.Info.ExclusiveStatus : "-",
                IsInline = true
            });

            embed.Fields.AddRange(BuildStatusEmbedFields(status.Statuses));
            embed.Fields.AddRange(BuildOtherEmbedFields(status.StatusOthers));

            return embed.Build();
        }

        public Embed BuildEmbedForOther(string name)
        {
            var embed = new EmbedBuilder();

            var other = statusService.BuildOtherInfoFromName(name);

            embed.Title = other.Info.Name;
            embed.ThumbnailUrl = "https://cdn.discordapp.com/attachments/717498404040605784/721817180877881394/FFRK_Misc.png";

            embed.AddField(new EmbedFieldBuilder()
            {
                Name = "Effects",
                Value = other.Info.Effects != string.Empty ? other.Info.Effects : "-"
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "School",
                Value = other.Info.School != string.Empty ? other.Info.School : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Element",
                Value = other.Info.Element != string.Empty ? other.Info.Element : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Type",
                Value = other.Info.Type != string.Empty ? other.Info.Type : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Target",
                Value = other.Info.Target != string.Empty ? other.Info.Target : "-",
                IsInline = true
            });

            embed.Fields.Add(new EmbedFieldBuilder()
            {
                Name = "Cast Time",
                Value = other.Info.Time != string.Empty ? other.Info.Time : "-",
                IsInline = true
            });

            embed.Fields.AddRange(BuildStatusEmbedFields(other.Statuses));
            embed.Fields.AddRange(BuildOtherEmbedFields(other.Others));

            return embed.Build();
        }

        public List<Embed> BuildEmbedsForRecordMaterias(string name)
        {
            var embeds = new List<Embed>();

            var recordMaterias = materiaService.BuildRecordMateriaInfoByName(name);

            foreach (var materia in recordMaterias)
            {
                var embed = new EmbedBuilder();

                embed.Title = materia.Info.Name;
                embed.ThumbnailUrl = materia.Info.MateriaImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = materia.Info.Effect != string.Empty ? materia.Info.Effect : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "How to Obtain",
                    Value = materia.Info.UnlockCriteria != string.Empty ? materia.Info.UnlockCriteria : "-"
                });


                embed.Fields.AddRange(BuildStatusEmbedFields(materia.Statuses));

                embeds.Add(embed.Build());
            }

            return embeds;
        }

        public List<Embed> BuildEmbedsForLegendMaterias(string name)
        {
            var embeds = new List<Embed>();

            var legendMaterias = materiaService.BuildLegendMateriaInfoByName(name);

            foreach (var materia in legendMaterias)
            {
                var embed = new EmbedBuilder();
                var relic = materia.Info.Relic == "-" ? "Dive" : materia.Info.Relic;
                embed.Title = $"{materia.Info.Name} ({relic})";
                embed.ThumbnailUrl = materia.Info.MateriaImage;

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Effects",
                    Value = materia.Info.Effect != string.Empty ? materia.Info.Effect : "-"
                });

                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "JP Name",
                    Value = materia.Info.JPName != string.Empty ? materia.Info.JPName : "-",
                    IsInline = true
                });

                if (materia.Info.Anima != null && materia.Info.Relic != "-")
                {
                    var animaValue = $"Yes (Wave: {materia.Info.Anima})";

                    if (materia.Info.Anima == settings.JpAnimaWave)
                    {
                        animaValue = $"JP Only (Wave: {materia.Info.Anima})";
                    }

                    if (materia.Info.Anima == "")
                    {
                        animaValue = "No";
                    }

                    embed.Fields.Add(new EmbedFieldBuilder()
                    {
                        Name = "Anima Available",
                        Value = animaValue,
                        IsInline = true
                    });
                }

                embed.Fields.AddRange(BuildStatusEmbedFields(materia.Statuses));

                embeds.Add(embed.Build());
            }

            return embeds;
        }

        IList<Soulbreak> GetPage(IList<Soulbreak> list, int page, int pageSize)
        {
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public string BuildSortedStringFromStats(Dictionary<string, int> statsDict)
        {
            var stats = statsDict.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToList();

            stats = stats.OrderByDescending(x => CustomStatSorter(x.Key)).ToList();

            string statString = string.Empty;

            foreach (var stat in stats)
            {
                var statValue = CustomStatSorter(stat.Key) == 0 ? $"{stat.Value}%" : stat.Value;

                statString += $"**{stat.Key.Replace("dam.", "Damage").Replace("ability", "Ability").Replace("heal", "Heal").Trim()}:** {statValue} \n";
            }

            return statString;
        }

        public int CustomStatSorter(string stat)
        {
            if (stat.ToLower().Contains("dam") || stat.ToLower().Contains("heal"))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public List<EmbedFieldBuilder> BuildStatusEmbedFields(Dictionary<string, List<SheetStatus>> statuses)
        {
            var statusFields = new List<EmbedFieldBuilder>();

            foreach (var statusSource in statuses)
            {
                foreach (var status in statusSource.Value)
                {
                    var time = status.DefaultDuration != "-" ? $"{status.DefaultDuration}s" : string.Empty;
                    var title = $":small_orange_diamond: Status: {status.Name}: {time}";

                    if(statusFields.Where(x => x.Name == title).Count() == 0)
                    {
                        var statusField = new EmbedFieldBuilder()
                        {
                            Name = title,
                            Value = status.Effects,
                            IsInline = false
                        };

                        statusFields.Add(statusField);
                    }                                   
                }
            }

            return statusFields;
        }

        public List<EmbedFieldBuilder> BuildOtherEmbedFields(Dictionary<string, List<SheetOthers>> others)
        {
            var otherFields = new List<EmbedFieldBuilder>();

            foreach (var otherSource in others)
            {
                foreach (var other in otherSource.Value)
                {
                    var otherField = new EmbedFieldBuilder()
                    {
                        Name = $":crossed_swords: Chase: {other.Name}",
                        Value = $"{other.Effects} \n | **Element**: {other.Element} " +
                        $"| **Type:** {other.Type} | **Target:** | {other.Target} | **Multiplier:** {other.Multiplier} | **Cast Time:** {other.Time} " +
                        $"| **SB Charge:** {other.SB} | **School:** {other.School}",
                        IsInline = false
                    };

                    otherFields.Add(otherField);

                    otherFields.AddRange(BuildStatusEmbedFields(other.OtherStatuses));
                }
            }

            return otherFields;
        }
    }
}
