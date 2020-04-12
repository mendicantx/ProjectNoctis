using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly FFRecordContext dbContext;
        private List<DbEquipment> dbEquipmentCache;
        private List<DbSkills> dbSkillsCache;

        public CharacterRepository(FFRecordContext context)
        {
            dbContext = context;
        }

        private void InitializeEquipmentAndSkillCache()
        {
            dbEquipmentCache = dbContext.Equipment.ToList();
            dbSkillsCache = dbContext.Skills.ToList();
        }

        public void UpdateCharactersFromSheet(IList<SheetCharacters> characters)
        {
            InitializeEquipmentAndSkillCache();
            var charactersToUpdate = new List<DbCharacters>();
            try
            {
                foreach (var c in characters)
                {
                    var characterToUpdate = dbContext.Characters.FirstOrDefault(x => x.CharacterId == c.CharacterId);

                    if (characterToUpdate == null)
                    {
                        characterToUpdate = new DbCharacters();
                    }

                    characterToUpdate.BaseDef = c.BaseDef;
                    characterToUpdate.BaseAcc = c.BaseAcc;
                    characterToUpdate.BaseAtk = c.BaseAtk;
                    characterToUpdate.BaseEva = c.BaseEva;
                    characterToUpdate.BaseHp = c.BaseHp;
                    characterToUpdate.BaseMag = c.BaseMag;
                    characterToUpdate.BaseSpd = c.BaseSpd;
                    characterToUpdate.BaseMnd = c.BaseMnd;
                    characterToUpdate.BaseRes = c.BaseRes;
                    characterToUpdate.Name = c.Name;
                    characterToUpdate.Realm = c.Realm;
                    characterToUpdate.CharacterId = c.CharacterId;

                    if(characterToUpdate.Id == 0)
                    {
                        dbContext.Characters.Add(characterToUpdate);
                    }
                    
                    foreach (var equip in c.Equipment)
                    {
                        var existingEquip = GetDbEquipment(equip);
                    }

                    foreach (var skill in c.Skills.Keys)
                    {
                        var existingSkill = GetDbSkills(new KeyValuePair<string, int>(skill, c.Skills[skill]));
                    }
                }
            }
            catch(Exception ex)
            {

            }
            

            dbContext.SaveChanges();

            UpdateCharacterEquipmentFromSheet(characters);
            UpdateCharacterSkillsFromSheet(characters);
        }


        public void UpdateCharacterEquipmentFromSheet(IList<SheetCharacters> characters)
        {
            foreach (var c in characters)
            {
                var characterToUpdate = dbContext.Characters.Include(x => x.CharacterEquipment).FirstOrDefault(x => x.CharacterId == c.CharacterId);

                if(characterToUpdate.CharacterEquipment == null)
                {
                    characterToUpdate.CharacterEquipment = new List<DbCharacterEquipment>();
                }

                foreach (var equip in c.Equipment)
                {
                    var existingEquip = GetDbEquipment(equip);
                    var currentEquip = characterToUpdate.CharacterEquipment.FirstOrDefault(x => x.EquipmentId == existingEquip.EquipmentId);

                    if (currentEquip == null)
                    {
                        currentEquip = new DbCharacterEquipment();
                        currentEquip.EquipmentId = existingEquip.EquipmentId;
                        currentEquip.Equipment = existingEquip;
                        currentEquip.Character = characterToUpdate;
                        currentEquip.CharacterId = characterToUpdate.Id;
                        characterToUpdate.CharacterEquipment.Add(currentEquip);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        public void UpdateCharacterSkillsFromSheet(IList<SheetCharacters> characters)
        {
            foreach (var c in characters)
            {
                var characterToUpdate = dbContext.Characters.Include(x => x.CharacterSkills).FirstOrDefault(x => x.CharacterId == c.CharacterId);

                if (characterToUpdate.CharacterSkills == null)
                {
                    characterToUpdate.CharacterSkills = new List<DbCharacterSkills>();
                }

                foreach (var skill in c.Skills.Keys)
                {
                    var existingSkill = GetDbSkills(new KeyValuePair<string, int>(skill, c.Skills[skill]));
                    var currentSkill = characterToUpdate.CharacterSkills.FirstOrDefault(x => x.SkillId == existingSkill.SkillId);

                    if (currentSkill == null)
                    {
                        currentSkill = new DbCharacterSkills();
                        currentSkill.SkillId = existingSkill.SkillId;
                        currentSkill.Skill = existingSkill;
                        currentSkill.Character = characterToUpdate;
                        currentSkill.CharacterId = characterToUpdate.Id;
                        characterToUpdate.CharacterSkills.Add(currentSkill);
                    }
                }
                
            }
        }
        public IList<DbCharacters> GetAllCharacters()
        {
            return dbContext.Characters.
                ToList();
        }

        public IList<string> GetAllCharacterNames()
        {
            return dbContext.Characters.AsNoTracking().Select(x => x.Name).ToList();
        }

        public DbCharacters GetCharacterByCharId(string characterId)
        {
            return dbContext.Characters.FirstOrDefault(x => x.CharacterId == characterId);
        }

        public void UpdateLegendSpheresFromSheet(IList<SheetLegendSpheres> legendSpheres)
        {
            foreach (var sphere in legendSpheres)
            {
                var currentSphere = dbContext.LegendSpheres.FirstOrDefault(x => x.Character == sphere.Character);

                if (currentSphere == null)
                {
                    currentSphere = new DbLegendSpheres();
                }

                currentSphere.Character = sphere.Character;
                currentSphere.Misc = String.Join("&", sphere.Misc);
                currentSphere.Stats = JsonSerializer.Serialize(sphere.Stats);
                currentSphere.MoteOne = sphere.MoteOne;
                currentSphere.MoteTwo = sphere.MoteTwo;

                if (currentSphere.Id == 0)
                {
                    dbContext.Add(currentSphere);
                }
                else
                {
                    dbContext.Update(currentSphere);
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateRecordSpheresFromSheet(IList<SheetRecordSpheres> recordSpheres)
        {
            foreach (var sphere in recordSpheres)
            {
                var currentSphere = dbContext.RecordSpheres.FirstOrDefault(x => x.Character == sphere.Character);

                if (currentSphere == null)
                {
                    currentSphere = new DbRecordSpheres();
                }

                currentSphere.Character = sphere.Character;
                currentSphere.Misc = String.Join("&", sphere.Misc);
                currentSphere.Stats = JsonSerializer.Serialize(sphere.Stats);

                if (currentSphere.Id == 0)
                {
                    dbContext.Add(currentSphere);
                }
                else
                {
                    dbContext.Update(currentSphere);
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateRecordBoardsFromSheet(IList<SheetRecordBoards> recordBoards)
        {
            foreach (var board in recordBoards)
            {
                var currentBoard = dbContext.RecordBoards.FirstOrDefault(x => x.Character == board.Character);

                if (currentBoard == null)
                {
                    currentBoard = new DbRecordBoards();
                }

                currentBoard.Character = board.Character;
                currentBoard.BoardMisc = String.Join("&", board.BoardMisc);
                currentBoard.BoardBonusStats = JsonSerializer.Serialize(board.BoardBonusStats);
                currentBoard.MotesRequired = JsonSerializer.Serialize(board.MotesRequired);

                if (currentBoard.Id == 0)
                {
                    dbContext.Add(currentBoard);
                }
                else
                {
                    dbContext.Update(currentBoard);
                }
            }

            dbContext.SaveChanges();
        }

        public DbCharacters GetCharacterByName(string name)
        {
            return dbContext.Characters.
                Include(x => x.RecordBoard).
                Include(x => x.RecordMaterias).
                Include(x => x.LegendMaterias).
                Include(x => x.LegendSphere).
                Include(x => x.Soulbreaks).
                Include(x => x.RecordSphere).
                FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

      

        private DbEquipment GetDbEquipment(string equipmentName)
        {
            var equipment = dbEquipmentCache.FirstOrDefault(x => x.Equipment == equipmentName);

            if (equipment == null)
            {
                if (equipment == null)
                {
                    equipment = new DbEquipment
                    {
                        Equipment = equipmentName
                    };

                    dbContext.Equipment.Add(equipment);
                }

                dbEquipmentCache.Add(equipment);
            }

            return equipment;
        }

        private DbSkills GetDbSkills(KeyValuePair<string,int> skill)
        {
            var currentSkill = dbSkillsCache.FirstOrDefault(x => x.SkillName == skill.Key && x.SkillValue == skill.Value );

            if (currentSkill == null)
            {
                if (currentSkill == null)
                {
                    currentSkill = new DbSkills
                    {
                        SkillValue = skill.Value,
                        SkillName = skill.Key
                    };

                    dbContext.Add(currentSkill);
                }

                dbSkillsCache.Add(currentSkill);
            }

            return currentSkill;
        }
    }
}
