using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using GoogleApiWrapper;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase
{
    public class FfrkSheetContext : IFfrkSheetContext
    {
        public static String spreadsheetId = "1f8OJIQhpycljDQ8QNDk_va1GJ1u7RVoMaNjFcHH0LKk";
        public bool initialUpdate = true;
        public static SheetsService service = new GoogleApi().Service;
        public Spreadsheet spreadsheet_meta = null;
        public List<SheetCharacters> Characters { get; set; }
        public List<SheetAbilities> Abilities { get; set; }
        public List<SheetHeroAbilities> HeroAbilities { get; set; }
        public List<SheetZenithAbilities> ZenithAbilities { get; set; }
        public List<SheetCrystalForceAbilities> CrystalForceAbilities { get; set; }
        public List<SheetMagicites> Magicites { get; set; }
        public List<SheetSoulbreaks> Soulbreaks { get; set; }
        public List<SheetStatus> Statuses { get; set; }
        public List<SheetOthers> Others { get; set; }
        public List<SheetSynchros> Synchros { get; set; }
        public List<SheetBraves> Braves { get; set; }
        public List<SheetBursts> Bursts { get; set; }
        public List<SheetGuardianSummons> GuardianSummons { get; set; }
        public List<SheetRecordMaterias> RecordMaterias { get; set; }
        public List<SheetLegendMaterias> LegendMaterias { get; set; }
        public List<SheetLegendSpheres> LegendSpheres { get; set; }
        public List<SheetRecordSpheres> RecordSpheres { get; set; }
        public List<SheetRecordBoards> RecordBoards { get; set; }
        public List<SheetLimitBreaks> LimitBreaks { get; set; }
        public List<SheetUniqueEquipment> UniqueEquipment { get; set; }
        public List<SheetUniqueEquipmentSets> UniqueEquipmentSets { get; set; }
        public List<SheetEvents> Events { get; set;}
        public bool LastUpdateSuccessful { get; set; }
        public DateTime LastUpdateTime { get; set; }


        public FfrkSheetContext()
        {
            while (!LastUpdateSuccessful)
            {
                LastUpdateSuccessful = SetupProperties().Result;

                if (!LastUpdateSuccessful)
                {
                    Console.WriteLine("Setting up properties failed, initiating sleep");
                    Thread.Sleep(60000);
                }
                else
                {
                    Console.WriteLine("Database Updated");
                }
            }
        }

        public async Task<bool> SetupProperties()
        {
            try
            {
                while (spreadsheet_meta == null)
                {
                    try
                    {
                        spreadsheet_meta = await service.Spreadsheets.Get(spreadsheetId).ExecuteAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed To Pull Sheet Retrying..." + ex.StackTrace);
                        Thread.Sleep(10000);
                    }
                }

                foreach (Sheet sheet in spreadsheet_meta.Sheets)
                {
                    string sheetName = sheet.Properties.Title;

                    if (sheetName != "Header" && sheetName != "Calculator")
                    {
                        IList<IList<object>> data = GetSpreadsheetData(sheetName).Result;
                        var headers = data[0];
                        data.Remove(data[0]);
                        switch (sheetName)
                        {
                            case "Events":
                                Console.WriteLine("Started Events ");
                                ParseEvents(data, headers);
                                Console.WriteLine("Updated Events ");
                                break;
                            case "Characters":
                                Console.WriteLine("Started Chars ");
                                ParseCharacters(data, headers);
                                Console.WriteLine("Updated Chars ");
                                break;
                            case "Abilities":
                                Console.WriteLine("Started Abils ");
                                ParseAbilities(data, headers);
                                Console.WriteLine("Updated Abils ");
                                break;
                            case "Hero Abilities":
                                Console.WriteLine("Started Hero Abils ");
                                ParseHeroAbilities(data, headers);
                                Console.WriteLine("Updated Hero Abils ");
                                break;
                            case "Magicite":
                                Console.WriteLine("Started Magicites");
                                ParseMagicites(data, headers);
                                Console.WriteLine("Updated Magicites");
                                break;
                            case "Soul Breaks":
                                Console.WriteLine("Started Sbs");
                                ParseSoulbreaks(data, headers);
                                Console.WriteLine("Updated Sbs");
                                break;
                            case "Limit Breaks":
                                Console.WriteLine("Started LBs");
                                ParseLimitBreaks(data, headers);
                                Console.WriteLine("Updated LBs");
                                break;
                            case "Status":
                                Console.WriteLine("Started Statuses");
                                ParseStatus(data, headers);
                                Console.WriteLine("Updated Statuses");
                                break;
                            case "Other":
                                Console.WriteLine("Started Others");
                                ParseOthers(data, headers);
                                Console.WriteLine("Updated Others");
                                break;
                            case "Synchro":
                                Console.WriteLine("Started Syncs");
                                ParseSynchros(data, headers);
                                Console.WriteLine("Updated Syncs");
                                break;
                            case "Brave":
                                Console.WriteLine("Started Braves ");
                                ParseBraves(data, headers);
                                Console.WriteLine("Updated Braves ");
                                break;
                            case "Burst":
                                Console.WriteLine("Started Burst");
                                ParseBursts(data, headers);
                                Console.WriteLine("Updated Burst");
                                break;
                            case "Record Materia":
                                Console.WriteLine("Started RMs");
                                ParseRecordMaterias(data, headers);
                                Console.WriteLine("Updated RMs");
                                break;
                            case "Legend Materia":
                                Console.WriteLine("Started LMs ");
                                ParseLegendMaterias(data, headers);
                                Console.WriteLine("Updated LMs ");
                                break;
                            case "Legend Spheres":
                                Console.WriteLine("Started LSs");
                                ParseLegendSpheres(data, headers);
                                Console.WriteLine("Updated LSs");
                                break;
                            case "Record Spheres":
                                Console.WriteLine("Started RSs");
                                ParseRecordSpheres(data, headers);
                                Console.WriteLine("Updated RSs");
                                break;
                            case "Record Board":
                                Console.WriteLine("Started RBs");
                                ParseRecordBoard(data, headers);
                                Console.WriteLine("Updated RBs");
                                break;
                            case "Guardian Summon Commands":
                                Console.WriteLine("Started Guardian Summons");
                                ParseGuardians(data, headers);
                                Console.WriteLine("Updated Guardian Summons");
                                break;
                            case "Hero Artifacts":
                                Console.WriteLine("Started Unique Equipment");
                                ParseUniqueEquipment(data, headers);
                                Console.WriteLine("Updated Unique Equipment");
                                break;
                            case "Hero Artifact Set Bonuses":
                                Console.WriteLine("Started Unique Equipment Sets");
                                ParseUniqueEquipmentSets(data, headers);
                                Console.WriteLine("Updated Unique Equipment Sets");
                                break;
                            case "Zenith SB Abilities":
                                Console.WriteLine("Started Zenith Abilities");
                                ParseZenithAbilities(data, headers);
                                Console.WriteLine("Updated Zenith Abilities");
                                break;
                            case "Crystal Force Abilities":
                                Console.WriteLine("Started Crystal Force Abilities");
                                ParseCrystalForceAbilities(data, headers);
                                Console.WriteLine("Updated Crystal Force Abilities");
                                break;
                            default:
                                break;
                        }

                    }
                }

                //bindAbilitiesToEventRelease();
                LastUpdateTime = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.TargetSite);
                return false;
            }
            
        }

        public void bindAbilitiesToEventRelease() {
            Console.WriteLine("Starting to Bind Abilities to Events");
            foreach (var ability in Abilities) {
                ability.IntroducingEvent = Events.Where(x => x.EventName == ability.IntroducingEventName).FirstOrDefault();
                if(ability.IntroducingEvent == null)
                    ability.IntroducingEvent = new SheetEvents { EventName = "Not found", JPDate = DateTime.MinValue, GLDate = DateTime.MinValue};
            }
            Console.WriteLine("Done Binding Abilities to Events");
        }
        public async Task<IList<IList<object>>> GetSpreadsheetData(string sheetName)
        {
            var data = await service.Spreadsheets.Values.Get(spreadsheetId, sheetName).ExecuteAsync();

            return data.Values;
        }

        public void ParseRecordBoard(IList<IList<object>> boardData, IList<object> headers)
        {
            var recordBoards = new List<SheetRecordBoards>();
            boardData = boardData.Where(x => x.Count() >= 5).ToList();
            var boards = boardData.GroupBy(x => x[1]);
            try
            {
                foreach (var board in boards)
                {
                    var characterStats = new Dictionary<string, int>();
                    var characterMisc = new List<string>();
                    var characterName = board.Key.ToString();
                    var boardMotes = new Dictionary<string, int>();

                    try
                    {
                        foreach (var characterBonus in board)
                        {

                            ParseBoardBonus(characterBonus, characterStats, characterMisc);
                            ParseBoardMotes(characterBonus, boardMotes);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Throwing out board for {characterName} due to bad data during update");
                        continue;
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
            catch (Exception)
            {
                Console.WriteLine($"Unhandled record boards error due to bad data during update");
            }

        }

        public void ParseBoardBonus(IList<object> board, Dictionary<string, int> bonusStats, List<string> misc)
        {

            var stringStat = board[2].ToString();
            if (stringStat.Contains("+"))
            {
                var statSplit = stringStat.Split("+");
                var statName = statSplit[0];
                var statValue = 0;
            
                if(!statSplit.Any(x => x.Contains("?")))
                {
                    statValue = Int32.Parse(new String(statSplit[1].Where(Char.IsDigit).ToArray()));
                }               

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

        public void ParseBoardMotes(IList<object> board, Dictionary<string, int> motes)
        {
            var boardCount = board.Count();
            if (motes.ContainsKey(board[4].ToString()))
            {
                motes[board[4].ToString()] += Int32.Parse(board[5].ToString());
            }
            else
            {
                motes.Add(board[4].ToString(), Int32.Parse(board[5].ToString()));
            }

            if (boardCount >= 8)
            {
                if (motes.ContainsKey(board[6].ToString()))
                {
                    motes[board[6].ToString()] += Int32.Parse(board[7].ToString());
                }
                else
                {
                    motes.Add(board[6].ToString(), Int32.Parse(board[7].ToString()));
                }
            }
            if (boardCount >= 10)
            {
                if (motes.ContainsKey(board[8].ToString()))
                {
                    motes[board[8].ToString()] += Int32.Parse(board[9].ToString());
                }
                else
                {
                    motes.Add(board[8].ToString(), Int32.Parse(board[9].ToString()));
                }
            }
            if (boardCount >= 12)
            {
                if (motes.ContainsKey(board[10].ToString()))
                {
                    motes[board[10].ToString()] += Int32.Parse(board[11].ToString());
                }
                else
                {
                    motes.Add(board[10].ToString(), Int32.Parse(board[11].ToString()));
                }
            }


        }

        public void ParseRecordSpheres(IList<IList<object>> recordData, IList<object> headers)
        {
            var characters = new List<SheetRecordSpheres>();
            var characterRecordSpheres = recordData.GroupBy(x => x[1]);

            foreach (var character in characterRecordSpheres)
            {
                try
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
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Record Sphere for {character.Key} due to bad data during update");
                    continue;
                }
                
            }
            RecordSpheres = characters;
        }
        public void ParseStatsFromSpheres(Dictionary<string, int> stats, List<string> misc, IList<object> sphere, int start, int end)
        {
            for (var i = start; i <= end; i++)
            {
                var sphereValue = sphere[i].ToString();

                if (sphereValue.Contains("+"))
                {
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
                else if (!sphereValue.Contains("Materia") && sphereValue != "" && sphereValue != "-")
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
                try
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
                        MoteOne = mote1,
                        MoteTwo = mote2

                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Legend Sphere for {character.Key} due to bad data during update");
                    continue;
                }
            }
            LegendSpheres = characters;
        }
        public void ParseLegendMaterias(IList<IList<object>> lmData, IList<object> headers)
        {
            var legendMaterias = new List<SheetLegendMaterias>();

            foreach (var lm in lmData)
            {
                if (lm.Count() < 10)
                    continue;

                try
                {
                    var animaIndex = headers.IndexOf("Anima");
                    var anima = animaIndex < lm.Count() ? lm[headers.IndexOf("Anima")].ToString() : "";

                    legendMaterias.Add(new SheetLegendMaterias
                    {
                        Name = lm[headers.IndexOf("Name")].ToString(),
                        Character = lm[headers.IndexOf("Character")].ToString(),
                        Effect = lm[headers.IndexOf("Effect")].ToString(),
                        Realm = lm[headers.IndexOf("Realm")].ToString(),
                        Master = lm[headers.IndexOf("Mastery Bonus")].ToString(),
                        JPName = lm[headers.IndexOf("Name (JP)")].ToString(),
                        LMId = lm[headers.IndexOf("ID")].ToString(),
                        LMVer = lm[headers.IndexOf("LM Ver")].ToString(),
                        Relic = lm[headers.IndexOf("Relic")].ToString(),
                        Anima = anima
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed To add LM: {string.Join(',', lm)}");
                    continue;
                }
                
            }

            LegendMaterias = legendMaterias;
        }

        public void ParseRecordMaterias(IList<IList<object>> rmData, IList<object> headers)
        {
            var recordMaterias = new List<SheetRecordMaterias>();

            foreach (var rm in rmData)
            {
                try
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
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Record Materia for {rm[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }               
            }

            RecordMaterias = recordMaterias;
        }
        public void ParseBursts(IList<IList<object>> burstData, IList<object> headers)
        {
            var bursts = new List<SheetBursts>();

            foreach (var burst in burstData)
            {
                if (burst.Count() < 20)
                    continue;
                try
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
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Burst for {burst[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
               
            }

            Bursts = bursts;
        }

        public void ParseUniqueEquipment(IList<IList<object>> equipmentData, IList<object> headers)
        {
            var equipments = new List<SheetUniqueEquipment>();

            foreach (var equip in equipmentData)
            {
                try
                {
                    equipments.Add(new SheetUniqueEquipment
                    {
                        Name = equip[headers.IndexOf("Name")].ToString(),
                        Character = equip[headers.IndexOf("Character")].ToString(),
                        Realm = equip[headers.IndexOf("Realm")].ToString(),
                        Season = equip[headers.IndexOf("Season")].ToString(),
                        Group = equip[headers.IndexOf("Group")].ToString(),
                        Type = equip[headers.IndexOf("Type")].ToString(),
                        Synergy = equip[headers.IndexOf("Synergy")].ToString(),
                        Combine = equip[headers.IndexOf("Combine")].ToString(),
                        Rarity = equip[headers.IndexOf("Rarity")].ToString(),
                        Level = equip[headers.IndexOf("Level")].ToString(),
                        Atk = equip[headers.IndexOf("ATK")].ToString(),
                        Def = equip[headers.IndexOf("DEF")].ToString(),
                        Mag = equip[headers.IndexOf("MAG")].ToString(),
                        Res = equip[headers.IndexOf("RES")].ToString(),
                        Mnd = equip[headers.IndexOf("MND")].ToString(),
                        Acc = equip[headers.IndexOf("ACC")].ToString(),
                        Eva = equip[headers.IndexOf("EVA")].ToString(),
                        FixedPassives = equip[headers.IndexOf("Fixed Passive Effects")].ToString(),
                        RandomPassives = equip[headers.IndexOf("Random Passive Effects")].ToString(),
                        Id = equip[headers.IndexOf("ID")].ToString()
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine($"Throwing out Unique Equipment for {equip[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }

            }

            UniqueEquipment = equipments;
        }

        public void ParseUniqueEquipmentSets(IList<IList<object>> equipmentData, IList<object> headers)
        {
            var equipments = new List<SheetUniqueEquipmentSets>();

            foreach (var equip in equipmentData)
            {
                try
                {
                    equipments.Add(new SheetUniqueEquipmentSets
                    {
                        Character = equip[headers.IndexOf("Character")].ToString(),
                        Realm = equip[headers.IndexOf("Realm")].ToString(),
                        Season = equip[headers.IndexOf("Season")].ToString(),
                        Group = equip[headers.IndexOf("Group")].ToString(),
                        TwoSetBonus = equip[headers.IndexOf("Set Bonus (2 Piece)")].ToString(),
                        ThreeSetBonus = equip[headers.IndexOf("Set Bonus (3 Piece)")].ToString()

                    });
                }
                catch (Exception)
                {
                    Console.WriteLine($"Throwing out Unique Equipment Set for {equip[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }

            }

            UniqueEquipmentSets = equipments;
        }

        public void ParseGuardians(IList<IList<object>> guardianData, IList<object> headers)
        {
            var guardians = new List<SheetGuardianSummons>();

            foreach (var guardian in guardianData)
            {
                if (guardian.Count() < 20)
                    continue;
                try
                {
                    guardians.Add(new SheetGuardianSummons
                    {
                        Name = guardian[headers.IndexOf("Name")].ToString(),
                        Character = guardian[headers.IndexOf("Character")].ToString(),
                        Effects = guardian[headers.IndexOf("Effects")].ToString(),
                        Element = guardian[headers.IndexOf("Element")].ToString(),
                        Formula = guardian[headers.IndexOf("Formula")].ToString(),
                        JPName = guardian[headers.IndexOf("Name (JP)")].ToString(),
                        Multiplier = guardian[headers.IndexOf("Multiplier")].ToString(),
                        SB = guardian[headers.IndexOf("SB")].ToString(),
                        Source = guardian[headers.IndexOf("Source")].ToString(),
                        School = guardian[headers.IndexOf("School")].ToString(),
                        GuardianSlot = guardian[headers.IndexOf("Guardian Ability Slot")].ToString(),
                        Target = guardian[headers.IndexOf("Target")].ToString(),
                        GuardianId = guardian[headers.IndexOf("ID")].ToString(),
                        Type = guardian[headers.IndexOf("Type")].ToString(),
                        Time = guardian[headers.IndexOf("Time")].ToString()
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine($"Throwing out guardian for {guardian[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
            }

            GuardianSummons = guardians;
        }

        public void ParseBraves(IList<IList<object>> braveData, IList<object> headers)
        {
            var braves = new List<SheetBraves>();

            foreach (var brave in braveData)
            {
                if (brave.Count() < 22)
                    continue;

                try 
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
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Brave for {brave[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
            }

            Braves = braves;
        }
        public void ParseEvents(IList<IList<object>> eventData, IList<object> headers)
        {
            var events = new List<SheetEvents>();

            foreach (var sheetEvent in eventData)
            {
                try 
                {
                    var glDateString = sheetEvent[headers.IndexOf("GL Date")].ToString();
                    var jpDateString = sheetEvent[headers.IndexOf("JP Date")].ToString();

                    DateTime glDate;
                    DateTime jpDate;

                    DateTime.TryParse(glDateString, out glDate);
                    DateTime.TryParse(jpDateString, out jpDate);

                    events.Add(new SheetEvents
                    {
                        EventName = sheetEvent[headers.IndexOf("Event Name")].ToString(),
                        GLDate = glDate,
                        JPDate = jpDate
                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Event for {sheetEvent[headers.IndexOf("Event Name")]} due to bad data during update");
                    continue;
                }
            }

            Events = events;
        }
        public void ParseSynchros(IList<IList<object>> synchroData, IList<object> headers)
        {
            var synchros = new List<SheetSynchros>();

            foreach (var synchro in synchroData)
            {
                if (synchro.Count() < 20)
                    continue;

                try
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
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Synchro for {synchro[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
                
            }
            Synchros = synchros;
        }
        public void ParseOthers(IList<IList<object>> otherData, IList<object> headers)
        {
            var others = new List<SheetOthers>();

            foreach (var other in otherData.Where(x => x.Count() >= 18))
            {
                try
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
                catch (Exception)
                {
                    Console.WriteLine($"Throwing out Other for {other[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
            }
            Others = others;
        }
        public void ParseStatus(IList<IList<object>> statusData, IList<object> headers)
        {
            var statuses = new List<SheetStatus>();

            foreach (var status in statusData.Where(x => x.Count() >= 6))
            {
                try
                {
                    statuses.Add(new SheetStatus
                    {
                        DefaultDuration = status[headers.IndexOf("Default Duration")].ToString(),
                        Effects = status[headers.IndexOf("Effects")].ToString(),
                        ExclusiveStatus = status[headers.IndexOf("Exclusive Status")].ToString(),
                        Name = status[headers.IndexOf("Common Name")].ToString(),
                        StatusId = status[headers.IndexOf("ID")].ToString()
                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Status {status[headers.IndexOf("Common Name")]} due to bad data during update");
                    continue;
                }
            }

            Statuses = statuses;
        }
        public void ParseSoulbreaks(IList<IList<object>> soulbreakData, IList<object> headers)
        {
            var soulbreaks = new List<SheetSoulbreaks>();

            foreach (var soulbreak in soulbreakData.Where(x => x.Count() >= 19))
            {
                try
                {
                    var animaIndex = headers.IndexOf("Anima");
                    var anima = animaIndex < soulbreak.Count() ? soulbreak[headers.IndexOf("Anima")].ToString() : "";
                    var newSoulbreak = new SheetSoulbreaks
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
                        SoulbreakBonus = soulbreak[headers.IndexOf("Mastery Bonus")].ToString(),
                        SoulbreakVersion = soulbreak[headers.IndexOf("SB Ver")].ToString(),
                        SoulbreakId = soulbreak[headers.IndexOf("ID")].ToString(),
                        Target = soulbreak[headers.IndexOf("Target")].ToString(),
                        Tier = soulbreak[headers.IndexOf("Tier")].ToString(),
                        Type = soulbreak[headers.IndexOf("Type")].ToString(),
                        Time = soulbreak[headers.IndexOf("Time")].ToString()
                    };

                    if (newSoulbreak.Name.Length == 0)
                        continue;
                        
                    soulbreaks.Add(newSoulbreak);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Soulbreak for {soulbreak[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
                
            }
            Soulbreaks = soulbreaks;
        }

        public void ParseLimitBreaks(IList<IList<object>> limitData, IList<object> headers)
        {
            var limits = new List<SheetLimitBreaks>();

            foreach (var limit in limitData.Where(x => x.Count() >= 19))
            {
                try
                {
                    limits.Add(new SheetLimitBreaks
                    {
                        Name = limit[headers.IndexOf("Name")].ToString(),
                        Character = limit[headers.IndexOf("Character")].ToString(),
                        Effects = limit[headers.IndexOf("Effects")].ToString(),
                        Element = limit[headers.IndexOf("Element")].ToString(),
                        Formula = limit[headers.IndexOf("Formula")].ToString(),
                        JPName = limit[headers.IndexOf("Name (JP)")].ToString(),
                        Multiplier = limit[headers.IndexOf("Multiplier")].ToString(),
                        Minimum = limit[headers.IndexOf("Minimum LB Points")].ToString(),
                        Realm = limit[headers.IndexOf("Realm")].ToString(),
                        Relic = limit[headers.IndexOf("Relic")].ToString(),
                        LimitBonus = limit[headers.IndexOf("Limit Break Bonus")].ToString(),
                        ID = limit[headers.IndexOf("ID")].ToString(),
                        Target = limit[headers.IndexOf("Target")].ToString(),
                        Tier = limit[headers.IndexOf("Tier")].ToString(),
                        Type = limit[headers.IndexOf("Type")].ToString(),
                        Time = limit[headers.IndexOf("Time")].ToString(),
                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out limit break for {limit[headers.IndexOf("Character")]} due to bad data during update");
                    continue;
                }
            }
            LimitBreaks = limits;
        }

        public void ParseMagicites(IList<IList<object>> magiciteData, IList<object> headers)
        {
            var magicites = new List<SheetMagicites>();

            foreach (var magicite in magiciteData.Where(x => x.Count() >= 56))
            {
                try
                {
                    magicites.Add(new SheetMagicites
                    {
                        Name = GetStringValueFromHeader(magicite, headers, "Name"),
                        JPName = GetStringValueFromHeader(magicite, headers, "Name (JP)"),
                        Effects = GetStringValueFromHeader(magicite, headers, "Effects"),
                        Element = GetStringValueFromHeader(magicite, headers, "Element"),
                        Formula = GetStringValueFromHeader(magicite, headers, "Formula"),
                        MagiciteId = GetStringValueFromHeader(magicite, headers, "ID"),
                        MagiciteUltra = GetStringValueFromHeader(magicite, headers, "Magicite Ultra Skill"),
                        Multiplier = GetStringValueFromHeader(magicite, headers, "Multiplier"),
                        Rarity = GetIntValueFromHeader(magicite, headers, "Rarity"),
                        UltraElement = GetStringValueFromHeader(magicite, headers, "Ultra Skill Element"),
                        Realm = GetStringValueFromHeader(magicite, headers, "Realm"),
                        Type = GetStringValueFromHeader(magicite, headers, "Type"),
                        Time = GetStringValueFromHeader(magicite, headers, "Time"),
                        Passives = ParseMagicitePassives(magicite, headers)

                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out magicite for {GetStringValueFromHeader(magicite, headers, "Name")} due to bad data during update");
                    continue;
                }
            }

            Magicites = magicites;
        }

        public Dictionary<string, int> ParseMagicitePassives(IList<object> magicite, IList<object> headers)
        {
            var passives = new Dictionary<string, int>();

            var passiveList = new List<string> { "Passive 1", "Passive 2", "Passive 3" };
            var i = 1;
            foreach (var passive in passiveList)
            {
                try
                {
                    var parsedPassive = magicite[headers.IndexOf(passive)].ToString();
                    var passiveIndex = i.ToString();
                    if (parsedPassive != "")
                    {
                        passives.Add(parsedPassive, Int32.Parse(magicite[headers.IndexOf($"{passiveIndex}-99")].ToString()));
                    }
                    i++;
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out magicite passive {magicite[headers.IndexOf(passive)]} due to bad data during update");
                    i++;
                }
                
            }

            return passives;
        }

        public string GetStringValueFromHeader(IList<object> item, IList<object> headers, string headerValue)
        {
            try
            {
                return item[headers.IndexOf(headerValue)].ToString();
            }
            catch
            {
                return null;
            }
        }

        public int GetIntValueFromHeader(IList<object> item, IList<object> headers, string headerValue)
        {
            try
            {
                return Int32.Parse(item[headers.IndexOf(headerValue)].ToString());
            }
            catch
            {
                return 0;
            }
        }
        public void ParseAbilities(IList<IList<object>> abilityData, IList<object> headers)
        {
            var abilities = new List<SheetAbilities>();

            foreach (var ability in abilityData)
            {
                try
                {
                    var newAbility = new SheetAbilities
                    {
                        Name = GetStringValueFromHeader(ability, headers, "Name"),
                        School = GetStringValueFromHeader(ability, headers, "School"),
                        SB = GetStringValueFromHeader(ability, headers, "SB"),
                        ID = GetStringValueFromHeader(ability, headers, "ID"),
                        AutoTarget = GetStringValueFromHeader(ability, headers, "Auto Target"),
                        Effects = GetStringValueFromHeader(ability, headers, "Effects"),
                        Element = GetStringValueFromHeader(ability, headers, "Element"),
                        Formula = GetStringValueFromHeader(ability, headers, "Formula"),
                        JPName = GetStringValueFromHeader(ability, headers, "Name (JP)"),
                        Max = GetStringValueFromHeader(ability, headers, "Max"),
                        Multiplier = GetStringValueFromHeader(ability, headers, "Multiplier"),
                        Rarity = GetStringValueFromHeader(ability, headers, "Rarity"),
                        Target = GetStringValueFromHeader(ability, headers, "Target"),
                        Time = GetStringValueFromHeader(ability, headers, "Time"),
                        Type = GetStringValueFromHeader(ability, headers, "Type"),
                        Uses = GetStringValueFromHeader(ability, headers, "Uses"),
                        IntroducingEventName = GetStringValueFromHeader(ability, headers, "Introducing Event"),
                        OrbsRequired = new Dictionary<string, string>(),
                        OrbCosts = new Dictionary<string, string>()
                    };

                    //Orb Required 1-4
                    for (int i = 1; i <= 4; i++)
                    {
                        var orbReq = GetStringValueFromHeader(ability, headers, $"Orb {i} Required");

                        if (orbReq != String.Empty && orbReq != null)
                        {
                            newAbility.OrbsRequired.Add($"{i}", orbReq);
                        }

                        //Ranks 1-5
                        for (int x = 1; x <= 5; x++)
                        {
                            var orbCost = GetStringValueFromHeader(ability, headers, $"{i}-R{x}");

                            if (orbCost != string.Empty && orbCost != null && orbCost != "0")
                            {
                                newAbility.OrbCosts.Add($"{i}-R{x}", orbCost);
                            }
                        }
                    }

                    abilities.Add(newAbility);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out ability {GetStringValueFromHeader(ability, headers, "Name")} due to bad data during update");
                    continue;
                }
            }

            this.Abilities = abilities;
        }

        public void ParseHeroAbilities(IList<IList<object>> abilityData, IList<object> headers)
        {
            var abilities = new List<SheetHeroAbilities>();

            foreach (var ability in abilityData)
            {
                try
                {
                    var newAbility = new SheetHeroAbilities
                    {
                        Character = GetStringValueFromHeader(ability, headers, "Character"),
                        Name = GetStringValueFromHeader(ability, headers, "Name"),
                        HAVersion = GetStringValueFromHeader(ability, headers, "HA Ver"),
                        Type = GetStringValueFromHeader(ability, headers, "Type"),
                        Target = GetStringValueFromHeader(ability, headers, "Target"),
                        Formula = GetStringValueFromHeader(ability, headers, "Formula"),
                        Multiplier = GetStringValueFromHeader(ability, headers, "Multiplier"),
                        Element = GetStringValueFromHeader(ability, headers, "Element"),
                        Time = GetStringValueFromHeader(ability, headers, "Time"),
                        Effects = GetStringValueFromHeader(ability, headers, "Effects"),
                        Counter = GetStringValueFromHeader(ability, headers, "Counter"),
                        AutoTarget = GetStringValueFromHeader(ability, headers, "Auto Target"),
                        SB = GetStringValueFromHeader(ability, headers, "SB"),
                        Uses = GetStringValueFromHeader(ability, headers, "Uses"),
                        Max = GetStringValueFromHeader(ability, headers, "Max"),
                        School = GetStringValueFromHeader(ability, headers, "School"),
                        JPName = GetStringValueFromHeader(ability, headers, "Name (JP)"),
                        ID = GetStringValueFromHeader(ability, headers, "ID"),
                        OrbsRequired = new Dictionary<string, string>(),
                        OrbCosts = new Dictionary<string, string>()
                    };

                    //Orb Required 1-4
                    for (int i = 1; i <= 3; i++)
                    {
                        var orbReq = GetStringValueFromHeader(ability, headers, $"Orb {i} Required");

                        if (orbReq != String.Empty && orbReq != null)
                        {
                            newAbility.OrbsRequired.Add($"{i}", orbReq);
                        }

                        //Ranks 1-5
                        for (int x = 1; x <= 5; x++)
                        {
                            var orbCost = GetStringValueFromHeader(ability, headers, $"{i}-R{x}");

                            if (orbCost != string.Empty && orbCost != null && orbCost != "0")
                            {
                                newAbility.OrbCosts.Add($"{i}-R{x}", orbCost);
                            }
                        }
                    }

                    abilities.Add(newAbility);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out ability {GetStringValueFromHeader(ability, headers, "Name")} due to bad data during update");
                    continue;
                }
            }

            this.HeroAbilities = abilities;
        }        
        public void ParseZenithAbilities(IList<IList<object>> abilityData, IList<object> headers)
        {
            var abilities = new List<SheetZenithAbilities>();

            foreach (var ability in abilityData)
            {
                try
                {
                    var newAbility = new SheetZenithAbilities
                    {
                        Name = GetStringValueFromHeader(ability, headers, "Name"),
                        School = GetStringValueFromHeader(ability, headers, "School"),
                        SB = GetStringValueFromHeader(ability, headers, "SB"),
                        ID = GetStringValueFromHeader(ability, headers, "ID"),
                        AutoTarget = GetStringValueFromHeader(ability, headers, "Auto Target"),
                        Effects = GetStringValueFromHeader(ability, headers, "Effects"),
                        Multiplier = GetStringValueFromHeader(ability, headers, "Multiplier"),
                        Element = GetStringValueFromHeader(ability, headers, "Element"),
                        Formula = GetStringValueFromHeader(ability, headers, "Formula"),
                        JPName = GetStringValueFromHeader(ability, headers, "Name (JP)"),
                        Target = GetStringValueFromHeader(ability, headers, "Target"),
                        Time = GetStringValueFromHeader(ability, headers, "Time"),
                        Type = GetStringValueFromHeader(ability, headers, "Type"),
                        Character = GetStringValueFromHeader(ability, headers, "Character"),
                        Source = GetStringValueFromHeader(ability, headers, "Source"),
                        SoulbreakVersion = GetStringValueFromHeader(ability, headers, "SB Ver")
                    };

                    if (newAbility.Source.Length == 0 || newAbility.Name.Length == 0)
                        continue;


                    abilities.Add(newAbility);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out zenith ability {GetStringValueFromHeader(ability, headers, "Name")} due to bad data during update");
                    continue;
                }
            }

            this.ZenithAbilities = abilities;
        }

        public void ParseCrystalForceAbilities(IList<IList<object>> abilityData, IList<object> headers)
        {
            var abilities = new List<SheetCrystalForceAbilities>();

            foreach (var ability in abilityData)
            {
                try
                {
                    var newAbility = new SheetCrystalForceAbilities
                    {
                        Name = GetStringValueFromHeader(ability, headers, "Name"),
                        School = GetStringValueFromHeader(ability, headers, "School"),
                        SB = GetStringValueFromHeader(ability, headers, "SB"),
                        ID = GetStringValueFromHeader(ability, headers, "ID"),
                        AutoTarget = GetStringValueFromHeader(ability, headers, "Auto Target"),
                        Effects = GetStringValueFromHeader(ability, headers, "Effects"),
                        Multiplier = GetStringValueFromHeader(ability, headers, "Multiplier"),
                        Element = GetStringValueFromHeader(ability, headers, "Element"),
                        Formula = GetStringValueFromHeader(ability, headers, "Formula"),
                        JPName = GetStringValueFromHeader(ability, headers, "Name (JP)"),
                        Target = GetStringValueFromHeader(ability, headers, "Target"),
                        Time = GetStringValueFromHeader(ability, headers, "Time"),
                        Type = GetStringValueFromHeader(ability, headers, "Type"),
                        Character = GetStringValueFromHeader(ability, headers, "Character"),
                        Source = GetStringValueFromHeader(ability, headers, "Source"),
                        SoulbreakVersion = GetStringValueFromHeader(ability, headers, "SB Ver")
                    };


                    abilities.Add(newAbility);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out Crystal Force ability {GetStringValueFromHeader(ability, headers, "Name")} for {GetStringValueFromHeader(ability, headers, "Character")} due to bad data during update");
                    continue;
                }
            }

            this.CrystalForceAbilities = abilities;
        }        
        public void ParseCharacters(IList<IList<object>> characterData, IList<Object> headers)
        {
            var characters = new List<SheetCharacters>();
            foreach (var character in characterData.Where(x => x.Count >= 104))
            {
                try
                {
                    characters.Add(new SheetCharacters
                    {
                        Realm = character[headers.IndexOf("Realm")].ToString(),
                        Name = character[headers.IndexOf("Name")].ToString(),
                        BaseAtk = Int32.Parse(character[headers.IndexOf("ATK - 99")].ToString() != "?" && character[headers.IndexOf("ATK - 99")].ToString() != "" ? character[headers.IndexOf("ATK - 99")].ToString() : "0"),
                        BaseAcc = Int32.Parse(character[headers.IndexOf("ACC - 99")].ToString() != "?" && character[headers.IndexOf("ACC - 99")].ToString() != "" ? character[headers.IndexOf("ACC - 99")].ToString() : "0"),
                        BaseSpd = Int32.Parse(character[headers.IndexOf("SPD - 99")].ToString() != "?" && character[headers.IndexOf("SPD - 99")].ToString() != "" ? character[headers.IndexOf("SPD - 99")].ToString() : "0"),
                        BaseDef = Int32.Parse(character[headers.IndexOf("DEF - 99")].ToString() != "?" && character[headers.IndexOf("DEF - 99")].ToString() != "" ? character[headers.IndexOf("DEF - 99")].ToString() : "0"),
                        BaseEva = Int32.Parse(character[headers.IndexOf("EVA - 99")].ToString() != "?" && character[headers.IndexOf("EVA - 99")].ToString() != "" ? character[headers.IndexOf("EVA - 99")].ToString() : "0"),
                        BaseHp = Int32.Parse(character[headers.IndexOf("HP - 99")].ToString() != "?" && character[headers.IndexOf("HP - 99")].ToString() != "" ? character[headers.IndexOf("HP - 99")].ToString() : "0"),
                        BaseMag = Int32.Parse(character[headers.IndexOf("MAG - 99")].ToString() != "?" && character[headers.IndexOf("MAG - 99")].ToString() != "" ? character[headers.IndexOf("MAG - 99")].ToString() : "0"),
                        BaseMnd = Int32.Parse(character[headers.IndexOf("MND - 99")].ToString() != "?" && character[headers.IndexOf("MND - 99")].ToString() != "" ? character[headers.IndexOf("MND - 99")].ToString() : "0"),
                        BaseRes = Int32.Parse(character[headers.IndexOf("RES - 99")].ToString() != "?" && character[headers.IndexOf("RES - 99")].ToString() != "" ? character[headers.IndexOf("RES - 99")].ToString() : "0"),
                        CharacterId = character[headers.IndexOf("ID")].ToString(),
                        Skills = ParseCharacterSkills(character, headers),
                        Equipment = ParseCharacterEquipment(character, headers)

                    });
                }
                catch(Exception)
                {
                    Console.WriteLine($"Throwing out character {character[headers.IndexOf("Name")]} due to bad data during update");
                    continue;
                }
            }

            this.Characters = characters;
        }

        public Dictionary<string, int> ParseCharacterSkills(IList<Object> character, IList<object> headers)
        {
            var skills = new Dictionary<string, int>();

            var skillList = new List<string>()
            {   "Black Magic", "White Magic", "Combat", "Support", "Celerity", "Summoning",
                "Spellblade", "Dragoon", "Monk", "Thief", "Knight", "Samurai","Ninja","Bard",
                "Dancer","Machinist", "Darkness", "Sharpshooter", "Witch", "Heavy"
            };


            foreach (var skill in skillList)
            {
                var characterSkill = character[headers.IndexOf(skill)].ToString();
                if (characterSkill != "")
                {
                    if (Int32.Parse(characterSkill) == 5 && Constants.Constants.nightmareEnhancedSkills.Contains(skill))
                    {
                        characterSkill = "6";
                    }
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

            foreach (var equipment in equipmentList)
            {
                var isEquipable = character[headers.IndexOf(equipment)].ToString();
                if (isEquipable == "Y")
                {
                    equipments.Add(equipment);
                }
            }
            return equipments;
        }
    }
}
