using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using GoogleApiWrapper;
using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase
{
    public class FfrkSheetContext
    {
        public static String spreadsheetId = "1f8OJIQhpycljDQ8QNDk_va1GJ1u7RVoMaNjFcHH0LKk";
        public static SheetsService service = new GoogleApi().Service;
        public Spreadsheet spreadsheet_meta = service.Spreadsheets.Get(spreadsheetId).Execute();
        public List<SheetCharacters> Characters { get; set; }
        public List<SheetAbilities> Abilities { get; set; }
        public List<SheetMagicites> Magicites { get; set; }
        public List<SheetSoulbreaks> Soulbreaks { get; set; }
        public List<SheetStatus> Statuses { get; set; }
        public List<SheetOthers> Others { get; set; }
        public List<SheetSynchros> Synchros { get; set; }
        public List<SheetBraves> Braves { get; set; }
        public List<SheetBursts> Bursts { get; set; }
        public List<SheetRecordMaterias> RecordMaterias { get; set; }
        public List<SheetLegendMaterias> LegendMaterias { get; set; }
        public List<SheetLegendSpheres> LegendSpheres { get; set; }
        public List<SheetRecordSpheres> RecordSpheres { get; set; }
        public List<SheetRecordBoards> RecordBoards { get; set; }
        public FfrkSheetContext()
        {
            foreach (Sheet sheet in spreadsheet_meta.Sheets)
            {
                string sheetName = sheet.Properties.Title;

                if (sheetName != "Header" && sheetName != "Calculator")
                {
                    
                    IList<IList<object>> data = service.Spreadsheets.Values.Get(spreadsheetId, sheetName).Execute().Values;
                    var headers = data[0];
                    data.Remove(data[0]);
                    switch (sheetName)
                    {
                        case "Characters":
                            ParseCharacters(data, headers);
                            break;
                        case "Abilities":
                            ParseAbilities(data, headers);
                            break;
                        case "Magicite":
                            ParseMagicites(data, headers);
                            break;
                        case "Soul Breaks":
                            ParseSoulbreaks(data, headers);
                            break;
                        case "Status":
                            ParseStatus(data, headers);
                            break;
                        case "Other":
                            ParseOthers(data, headers);
                            break;
                        case "Synchro":
                            ParseSynchros(data, headers);
                            break;
                        case "Brave":
                            ParseBraves(data, headers);
                            break;
                        case "Burst":
                            ParseBursts(data, headers);
                            break;
                        case "Record Materia":
                            ParseRecordMaterias(data, headers);
                            break;
                        case "Legend Materia":
                            ParseLegendMaterias(data, headers);
                            break;
                        case "Legend Spheres":
                            ParseLegendSpheres(data, headers);
                            break;
                        case "Record Spheres":
                            ParseRecordSpheres(data, headers);
                            break;
                        case "Record Board":
                            ParseRecordBoard(data, headers);
                            break;
                        default:
                            break;
                    }
                   
                }
            }
        }

        public void ParseRecordBoard (IList<IList<object>> boardData, IList<object> headers)
        {
            var recordBoards = new List<SheetRecordBoards>();
            boardData = boardData.Where(x => x.Count() >= 5).ToList();
            var boards = boardData.GroupBy(x => x[1]);

            foreach (var board in boards)
            {
                var characterStats = new Dictionary<string, int>();
                var characterMisc = new List<string>();
                var characterName = board.Key.ToString();
                var boardMotes = new Dictionary<string, int>();
                
                foreach(var characterBonus in board)
                {
                    ParseBoardBonus(characterBonus, characterStats, characterMisc);
                    ParseBoardMotes(characterBonus, boardMotes);
                }

                recordBoards.Add(new SheetRecordBoards
                {
                    BoardBonusStats = characterStats,
                    BoardMisc = characterMisc,
                    Character = characterName,
                    MotesRequired = boardMotes
                });
            }

            RecordBoards = recordBoards;
        }

        public void ParseBoardBonus(IList<object> board, Dictionary<string,int> bonusStats, List<string> misc)
        {
           
            var stringStat = board[2].ToString();
            if (stringStat.Contains("+"))
            {
                var statSplit = stringStat.Split("+");
                var statName = statSplit[0];
                var statValue = Int32.Parse(new String(statSplit[1].Where(Char.IsDigit).ToArray()));

                if (bonusStats.ContainsKey(statName))
                {
                    bonusStats[statName] += statValue;
                }
                else
                {
                    bonusStats.Add(statName, statValue);
                }
            }
            else
            {
                misc.Add(stringStat);
            }
        }

        public void ParseBoardMotes(IList<object> board, Dictionary<string,int> motes)
        {
            var boardCount = board.Count();
            if (motes.ContainsKey(board[3].ToString()))
            {
                motes[board[3].ToString()] += Int32.Parse(board[4].ToString());
            }
            else
            {
                motes.Add(board[3].ToString(), Int32.Parse(board[4].ToString()));
            }

            if(boardCount >= 7)
            {
                if (motes.ContainsKey(board[5].ToString()))
                {
                    motes[board[5].ToString()] += Int32.Parse(board[6].ToString());
                }
                else
                {
                    motes.Add(board[5].ToString(), Int32.Parse(board[6].ToString()));
                }
            }
            if (boardCount >= 9)
            {
                if (motes.ContainsKey(board[7].ToString()))
                {
                    motes[board[7].ToString()] += Int32.Parse(board[8].ToString());
                }
                else
                {
                    motes.Add(board[7].ToString(), Int32.Parse(board[8].ToString()));
                }
            }
            if (boardCount >= 11)
            {
                if (motes.ContainsKey(board[9].ToString()))
                {
                    motes[board[9].ToString()] += Int32.Parse(board[10].ToString());
                }
                else
                {
                    motes.Add(board[10].ToString(), Int32.Parse(board[10].ToString()));
                }
            }


        }

        public void ParseRecordSpheres(IList<IList<object>> recordData, IList<object> headers)
        {
            var characters = new List<SheetRecordSpheres>();
            var characterRecordSpheres = recordData.GroupBy(x => x[1]);

            foreach (var character in characterRecordSpheres)
            {
                var characterStats = new Dictionary<string, int>();
                var characterMisc = new List<string>();
                var characterName = character.Key.ToString();

                foreach (var recordSphere in character)
                {
                    ParseStatsFromSpheres(characterStats, characterMisc, recordSphere, 3, 7);
                }
                characters.Add(new SheetRecordSpheres
                {
                    Character = characterName,
                    Misc = characterMisc,
                    Stats = characterStats
                });
            }
            RecordSpheres = characters;
        }
        public void ParseStatsFromSpheres(Dictionary<string,int> stats, List<string> misc, IList<object> sphere, int start, int end)
        {
            for(var i = start; i < end; i++)
            {
                var sphereValue = sphere[i].ToString();

                if (sphereValue.Contains("+")){
                    var sphereSplit = sphereValue.Split("+");
                    var statName = sphereSplit[0];
                    var statValue = Int32.Parse(new String(sphereSplit[1].Where(Char.IsDigit).ToArray()));

                    if (stats.ContainsKey(statName))
                    {
                        stats[statName] += statValue;
                    }
                    else
                    {
                        stats.Add(statName, statValue);
                    }
                }
                else if(!sphereValue.Contains("Materia") && sphereValue != "" && sphereValue != "-")
                {
                    misc.Add(sphereValue);
                }
            }
        }
        public void ParseLegendSpheres(IList<IList<object>> legendData, IList<object> headers)
        {
            var characters = new List<SheetLegendSpheres>();
            var characterLegendSpheres = legendData.GroupBy(x => x[1]);

            foreach (var character in characterLegendSpheres)
            {
                var characterStats = new Dictionary<string, int>();
                var characterMisc = new List<string>();
                string mote1 = "";
                string mote2 = "";
                var characterName = character.Key.ToString();

                foreach (var legendSphere in character)
                {
                    ParseStatsFromSpheres(characterStats, characterMisc, legendSphere, 2, 6);
                    mote1 = legendSphere[headers.IndexOf("Mote 1 Required")].ToString();
                    mote2 = legendSphere[headers.IndexOf("Mote 2 Required")].ToString();
                }
                characters.Add(new SheetLegendSpheres
                {
                    Character = characterName,
                    Misc = characterMisc,
                    Stats = characterStats,
                    MoteOne = mote1

                });
            }
            LegendSpheres = characters;
        }
        public void ParseLegendMaterias(IList<IList<object>> lmData, IList<object> headers)
        {
            var legendMaterias = new List<SheetLegendMaterias>();

            foreach (var lm in lmData)
            {
                var animaIndex = headers.IndexOf("Anima");
                var anima = animaIndex < lm.Count() ? lm[headers.IndexOf("Anima")].ToString() : "";

                legendMaterias.Add(new SheetLegendMaterias
                {
                    Name = lm[headers.IndexOf("Name")].ToString(),
                    Character = lm[headers.IndexOf("Character")].ToString(),
                    Effect = lm[headers.IndexOf("Effect")].ToString(),
                    Realm = lm[headers.IndexOf("Realm")].ToString(),
                    Master = lm[headers.IndexOf("Master")].ToString(),
                    JPName = lm[headers.IndexOf("Name (JP)")].ToString(),
                    LMId = lm[headers.IndexOf("ID")].ToString(),
                    Relic = lm[headers.IndexOf("Relic")].ToString(),
                    Anima = anima
                });
            }

            LegendMaterias = legendMaterias;
        }

        public void ParseRecordMaterias(IList<IList<object>> rmData, IList<object> headers)
        {
            var recordMaterias = new List<SheetRecordMaterias>();

            foreach (var rm in rmData)
            {
                recordMaterias.Add(new SheetRecordMaterias
                {
                    Name = rm[headers.IndexOf("Name")].ToString(),
                    Character = rm[headers.IndexOf("Character")].ToString(),
                    Effect = rm[headers.IndexOf("Effect")].ToString(),
                    Realm = rm[headers.IndexOf("Realm")].ToString(),
                    UnlockCriteria = rm[headers.IndexOf("Unlock Criteria")].ToString(),
                    JPName = rm[headers.IndexOf("Name (JP)")].ToString(),
                    RMId = rm[headers.IndexOf("ID")].ToString()
           
                });
            }

            RecordMaterias = recordMaterias;
        }
        public void ParseBursts(IList<IList<object>> burstData, IList<object> headers)
        {
            var bursts = new List<SheetBursts>();

            foreach (var burst in burstData)
            {
                bursts.Add(new SheetBursts
                {
                    Name = burst[headers.IndexOf("Name")].ToString(),
                    Character = burst[headers.IndexOf("Character")].ToString(),
                    Effects = burst[headers.IndexOf("Effects")].ToString(),
                    Element = burst[headers.IndexOf("Element")].ToString(),
                    Formula = burst[headers.IndexOf("Formula")].ToString(),
                    JPName = burst[headers.IndexOf("Name (JP)")].ToString(),
                    Multiplier = burst[headers.IndexOf("Multiplier")].ToString(),
                    SB = burst[headers.IndexOf("SB")].ToString(),
                    Source = burst[headers.IndexOf("Source")].ToString(),
                    School = burst[headers.IndexOf("School")].ToString(),
                    Target = burst[headers.IndexOf("Target")].ToString(),
                    BurstId = burst[headers.IndexOf("ID")].ToString(),
                    Type = burst[headers.IndexOf("Type")].ToString(),
                    Time = burst[headers.IndexOf("Time")].ToString()
                });
            }

            Bursts = bursts;
        }
        public void ParseBraves(IList<IList<object>> braveData, IList<object> headers)
        {
            var braves = new List<SheetBraves>();

            foreach (var brave in braveData)
            {
                braves.Add(new SheetBraves
                {
                    Name = brave[headers.IndexOf("Name")].ToString(),
                    Character = brave[headers.IndexOf("Character")].ToString(),
                    Effects = brave[headers.IndexOf("Effects")].ToString(),
                    Element = brave[headers.IndexOf("Element")].ToString(),
                    Formula = brave[headers.IndexOf("Formula")].ToString(),
                    JPName = brave[headers.IndexOf("Name (JP)")].ToString(),
                    Multiplier = brave[headers.IndexOf("Multiplier")].ToString(),
                    SB = brave[headers.IndexOf("SB")].ToString(),
                    Source = brave[headers.IndexOf("Source")].ToString(),
                    School = brave[headers.IndexOf("School")].ToString(),
                    BraveCondition = brave[headers.IndexOf("Brave Condition")].ToString(),
                    BraveLevel = brave[headers.IndexOf("Brave")].ToString(),
                    Target = brave[headers.IndexOf("Target")].ToString(),
                    BraveId = brave[headers.IndexOf("ID")].ToString(),
                    Type = brave[headers.IndexOf("Type")].ToString(),
                    Time = brave[headers.IndexOf("Time")].ToString()
                });
            }

            Braves = braves;
        }
        public void ParseSynchros(IList<IList<object>> synchroData, IList<object> headers)
        {
            var synchros = new List<SheetSynchros>();
            
            foreach(var synchro in synchroData)
            {
                synchros.Add(new SheetSynchros
                {
                    Name = synchro[headers.IndexOf("Name")].ToString(),
                    Character = synchro[headers.IndexOf("Character")].ToString(),
                    Effects = synchro[headers.IndexOf("Effects")].ToString(),
                    Element = synchro[headers.IndexOf("Element")].ToString(),
                    Formula = synchro[headers.IndexOf("Formula")].ToString(),
                    JPName = synchro[headers.IndexOf("Name (JP)")].ToString(),
                    Multiplier = synchro[headers.IndexOf("Multiplier")].ToString(),
                    SB = synchro[headers.IndexOf("SB")].ToString(),
                    Source = synchro[headers.IndexOf("Source")].ToString(),
                    School = synchro[headers.IndexOf("School")].ToString(),
                    SynchroCondition = synchro[headers.IndexOf("Synchro Condition")].ToString(),
                    SynchroConditionId = synchro[headers.IndexOf("Synchro Condition ID")].ToString(),
                    Target = synchro[headers.IndexOf("Target")].ToString(),
                    SynchroId = synchro[headers.IndexOf("ID")].ToString(),
                    Type = synchro[headers.IndexOf("Type")].ToString(),
                    SynchroSlot = synchro[headers.IndexOf("Synchro Ability Slot")].ToString(),
                    Time = synchro[headers.IndexOf("Time")].ToString()
                });
            }
            Synchros = synchros;
        }
        public void ParseOthers(IList<IList<object>> otherData, IList<object> headers)
        {
            var others = new List<SheetOthers>();

            foreach(var other in otherData)
            {
                others.Add(new SheetOthers 
                {
                    Name = other[headers.IndexOf("Name")].ToString(),
                    SB = other[headers.IndexOf("SB")].ToString(),
                    Effects = other[headers.IndexOf("Effects")].ToString(),
                    Element = other[headers.IndexOf("Element")].ToString(),
                    School = other[headers.IndexOf("School")].ToString(),
                    OtherId = other[headers.IndexOf("ID")].ToString(),
                    Source = other[headers.IndexOf("Source")].ToString(),
                    Multiplier = other[headers.IndexOf("Multiplier")].ToString(),
                    Formula = other[headers.IndexOf("Formula")].ToString(),
                    SourceType = other[headers.IndexOf("Source Type")].ToString(),
                    Target = other[headers.IndexOf("Target")].ToString(),
                    Type = other[headers.IndexOf("Type")].ToString(),
                    Time = other[headers.IndexOf("Time")].ToString(),                  
                });

            }
            Others = others;
        }
        public void ParseStatus(IList<IList<object>> statusData,IList<object> headers)
        {
            var statuses = new List<SheetStatus>();

            foreach(var status in statusData.Where(x => x.Count() >= 7))
            {
                statuses.Add(new SheetStatus
                {
                    DefaultDuration = status[headers.IndexOf("Default Duration")].ToString(),
                    Effects = status[headers.IndexOf("Effects")].ToString(),
                    ExclusiveStatus = status[headers.IndexOf("Exclusive Status")].ToString(),
                    MindModifier = status[headers.IndexOf("MND Modifier")].ToString(),
                    Name = status[headers.IndexOf("Common Name")].ToString(),
                    StatusId = status[headers.IndexOf("ID")].ToString()
                });
            }

            Statuses = statuses;
        }
        public void ParseSoulbreaks(IList<IList<object>> soulbreakData, IList<object> headers)
        {
            var soulbreaks = new List<SheetSoulbreaks>();

            foreach(var soulbreak in soulbreakData.Where(x => x.Count() >= 19))
            {
                var animaIndex = headers.IndexOf("Anima");
                var anima = animaIndex < soulbreak.Count() ? soulbreak[headers.IndexOf("Anima")].ToString() : "";
                soulbreaks.Add(new SheetSoulbreaks
                {
                    Name = soulbreak[headers.IndexOf("Name")].ToString(),
                    Anima = anima,
                    Character = soulbreak[headers.IndexOf("Character")].ToString(),
                    Effects = soulbreak[headers.IndexOf("Effects")].ToString(),
                    Element = soulbreak[headers.IndexOf("Element")].ToString(),
                    Formula = soulbreak[headers.IndexOf("Formula")].ToString(),
                    JPName = soulbreak[headers.IndexOf("Name (JP)")].ToString(),
                    Multiplier = soulbreak[headers.IndexOf("Multiplier")].ToString(), 
                    Points = soulbreak[headers.IndexOf("Points")].ToString(),
                    Realm = soulbreak[headers.IndexOf("Realm")].ToString(),
                    Relic = soulbreak[headers.IndexOf("Relic")].ToString(),
                    SoulbreakBonus = soulbreak[headers.IndexOf("Soulbreak Bonus")].ToString(),
                    SoulbreakId = soulbreak[headers.IndexOf("ID")].ToString(),
                    Target = soulbreak[headers.IndexOf("Target")].ToString(),
                    Tier = soulbreak[headers.IndexOf("Tier")].ToString(),
                    Type = soulbreak[headers.IndexOf("Type")].ToString()
                });
            }
            Soulbreaks = soulbreaks;
        }
        public void ParseMagicites(IList<IList<object>> magiciteData, IList<object> headers)
        {
            var magicites = new List<SheetMagicites>();

            foreach(var magicite in magiciteData.Where(x => x.Count() >= 56))
            {
                magicites.Add(new SheetMagicites
                {
                    Name = magicite[headers.IndexOf("Name")].ToString(),
                    JPName = magicite[headers.IndexOf("Name (JP)")].ToString(),
                    Effects = magicite[headers.IndexOf("Effects")].ToString(),
                    Element = magicite[headers.IndexOf("Element")].ToString(),
                    Formula = magicite[headers.IndexOf("Formula")].ToString(),
                    MagiciteId = magicite[headers.IndexOf("ID")].ToString(),
                    MagiciteUltra = magicite[headers.IndexOf("Magicite Ultra Skill")].ToString(),
                    Multiplier = magicite[headers.IndexOf("Multiplier")].ToString(),
                    Rarity = Int32.Parse(magicite[headers.IndexOf("Rarity")].ToString()),
                    UltraElement = magicite[headers.IndexOf("Ultra Skill Element")].ToString(),
                    Realm = magicite[headers.IndexOf("Realm")].ToString(),
                    Type = magicite[headers.IndexOf("Type")].ToString(),
                    Time = magicite[headers.IndexOf("Time")].ToString(),
                    Passives = ParseMagicitePassives(magicite, headers)

                });
            }

            Magicites = magicites;
        }

        public Dictionary<string,int> ParseMagicitePassives(IList<object> magicite, IList<object> headers)
        {
            var passives = new Dictionary<string, int>();

            var passiveList = new List<string> { "Passive 1", "Passive 2", "Passive 3" };
            var i = 1;
            foreach (var passive in passiveList)
            {
                
                var parsedPassive = magicite[headers.IndexOf(passive)].ToString();
                var passiveIndex = i.ToString();
                if(parsedPassive != "")
                {
                    passives.Add(parsedPassive, Int32.Parse(magicite[headers.IndexOf($"{passiveIndex}-99")].ToString()));
                }
                i++;
            }

            return passives;
        }
        public void ParseAbilities(IList<IList<object>> abilityData, IList<object> headers)
        {
            var abilities = new List<SheetAbilities>();

            foreach(var ability in abilityData)
            {
                abilities.Add(new SheetAbilities
                {
                    Name = ability[headers.IndexOf("Name")].ToString(),
                    School = ability[headers.IndexOf("School")].ToString(),
                    SB = ability[headers.IndexOf("SB")].ToString(),
                    ID = ability[headers.IndexOf("ID")].ToString(),
                    AutoTarget = ability[headers.IndexOf("Auto Target")].ToString(),
                    Effects = ability[headers.IndexOf("Effects")].ToString(),
                    Element = ability[headers.IndexOf("Element")].ToString(),
                    Formula = ability[headers.IndexOf("Formula")].ToString(),
                    JPName = ability[headers.IndexOf("Name (JP)")].ToString(),
                    Max = ability[headers.IndexOf("Max")].ToString(),
                    Multiplier = ability[headers.IndexOf("Multiplier")].ToString(),
                    Rarity = ability[headers.IndexOf("Rarity")].ToString(),
                    Target = ability[headers.IndexOf("Target")].ToString(),
                    Time = ability[headers.IndexOf("Time")].ToString(),
                    Type = ability[headers.IndexOf("Type")].ToString(),
                    Uses = ability[headers.IndexOf("Uses")].ToString()
                });
            }

            this.Abilities = abilities;
        }
        public void ParseCharacters(IList<IList<object>> characterData, IList<Object> headers) 
        {
            var characters = new List<SheetCharacters>();
            foreach (var character in characterData.Where(x => x.Count == 108))
            {
                characters.Add(new SheetCharacters
                {
                    Realm = character[headers.IndexOf("Realm")].ToString(),
                    Name = character[headers.IndexOf("Name")].ToString(),
                    BaseAtk = Int32.Parse(character[headers.IndexOf("ATK - 99")].ToString() != "?" ? character[headers.IndexOf("ATK - 99")].ToString() : "0"),
                    BaseAcc = Int32.Parse(character[headers.IndexOf("ACC - 99")].ToString() != "?" ? character[headers.IndexOf("ACC - 99")].ToString() : "0"),
                    BaseSpd = Int32.Parse(character[headers.IndexOf("SPD - 99")].ToString() != "?" ? character[headers.IndexOf("SPD - 99")].ToString() : "0"),
                    BaseDef = Int32.Parse(character[headers.IndexOf("DEF - 99")].ToString() != "?" ? character[headers.IndexOf("DEF - 99")].ToString() : "0"),
                    BaseEva = Int32.Parse(character[headers.IndexOf("EVA - 99")].ToString() != "?" ? character[headers.IndexOf("EVA - 99")].ToString() : "0"),
                    BaseHp = Int32.Parse(character[headers.IndexOf("HP - 99")].ToString()   != "?" ? character[headers.IndexOf("HP - 99")].ToString() : "0"),
                    BaseMag = Int32.Parse(character[headers.IndexOf("MAG - 99")].ToString() != "?" ? character[headers.IndexOf("MAG - 99")].ToString() : "0"),
                    BaseMnd = Int32.Parse(character[headers.IndexOf("MND - 99")].ToString() != "?" ? character[headers.IndexOf("MND - 99")].ToString() : "0"),
                    BaseRes = Int32.Parse(character[headers.IndexOf("RES - 99")].ToString() != "?" ? character[headers.IndexOf("RES - 99")].ToString() : "0"),
                    CharacterId = character[headers.IndexOf("ID")].ToString(),
                    Skills = ParseCharacterSkills(character,headers),
                    Equipment = ParseCharacterEquipment(character,headers)

                });
            }

            this.Characters = characters;
        }

        public Dictionary<string,int> ParseCharacterSkills(IList<Object> character,IList<object> headers)
        {
            var skills = new Dictionary<string, int>();

            var skillList = new List<string>() 
            {   "Black Magic", "White Magic", "Combat", "Support", "Celerity", "Summoning",
                "Spellblade", "Dragoon", "Monk", "Thief", "Knight", "Samurai","Ninja","Bard",
                "Dancer","Machinist", "Darkness", "Sharpshooter", "Witch", "Heavy" 
            };

            foreach(var skill in skillList)
            {
                var characterSkill = character[headers.IndexOf(skill)].ToString();
                if(characterSkill != "")
                {
                    skills.Add(skill, Int32.Parse(characterSkill));
                }
            }
            return skills;
        }
        public List<string> ParseCharacterEquipment(IList<object> character, IList<object> headers)
        {
            var equipments = new List<string>();

            var equipmentList = new List<string>()
            {
                "Dagger", "Sword", "Katana", "Axe", "Hammer", "Spear", "Fist", "Rod", "Staff",  
                "Bow", "Instrument", "Whip", "Thrown", "Gun", "Book", "Blitzball","Hairpin", 
                "Gun-arm", "Gambling Gear", "Doll", "Keyblade", "Shield", "Hat", 
                "Helm", "Light Armor", "Heavy Armor", "Robe", "Bracer", "Accessory"
            };

            foreach(var equipment in equipmentList)
            {
                var isEquipable = character[headers.IndexOf(equipment)].ToString();
                if(isEquipable == "Y")
                {
                    equipments.Add(equipment);
                }
            }
            return equipments;
        }
    }
}
