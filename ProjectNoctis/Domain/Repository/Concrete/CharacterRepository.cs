using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class CharacterRepository : ICharacterRepository
    {
        private readonly FFRecordContext dbContext;
        public CharacterRepository(FFRecordContext context)
        {
            dbContext = context;
        }


        public void UpdateCharactersFromSheet(IList<SheetCharacters> characters)
        {
            var charactersToUpdate = new List<DbCharacters>();
            foreach (var c in characters)
            {
                var characterToUpdate = GetCharacterByName(c.Name);

                if (characterToUpdate == null)
                {

                    var character = new DbCharacters
                    {
                        BaseDef = c.BaseDef,
                        BaseAcc = c.BaseAcc,
                        BaseAtk = c.BaseAtk,
                        BaseEva = c.BaseEva,
                        BaseHp = c.BaseHp,
                        BaseMag = c.BaseMag,
                        BaseMnd = c.BaseMnd,
                        BaseRes = c.BaseRes,
                        BaseSpd = c.BaseSpd,
                        CharacterId = c.CharacterId,
                        Name = c.Name,
                        Realm = c.Realm,

                    };

                    character.Equipment = UpdateOrAddEquipmentFromSheet(c.Equipment).
                        Select(x => new DbCharacterEquipment { Equipment = x, EquipmentId = x.EquipmentId, Character = character }).
                        ToList();

                    character.Skills = UpdateOrAddSkillsFromSheet(c.Skills).
                        Select(x => new DbCharacterSkills { Skill = x, SkillId = x.SkillId, Character = character }).
                        ToList();

                    dbContext.Add(character);
                }
                else
                {
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
                    var characterSkills = UpdateOrAddSkillsFromSheet(c.Skills);
                    var characterEquips = UpdateOrAddEquipmentFromSheet(c.Equipment);

                    foreach (var equip in characterEquips)
                    {
                        DbCharacterEquipment matchedCharacterEquip = null;
                        if (characterToUpdate.Equipment != null && characterToUpdate.Equipment.Count() != 0)
                        {
                            matchedCharacterEquip = characterToUpdate.Equipment.FirstOrDefault(x => x.Equipment == equip);
                        }
                        else
                        {
                            characterToUpdate.Equipment = new List<DbCharacterEquipment>();
                        }

                        if (matchedCharacterEquip == null)
                        {
                            characterToUpdate.Equipment.Add(new DbCharacterEquipment
                            {
                                Character = characterToUpdate,
                                CharacterId = characterToUpdate.Id,
                                Equipment = equip,
                                EquipmentId = equip.EquipmentId
                            });
                        }
                        else
                        {
                            matchedCharacterEquip.Equipment = equip;
                        }
                    }

                    foreach (var skill in characterSkills)
                    {
                        DbCharacterSkills matchedSkill = null;

                        if (characterToUpdate.Skills != null)
                        {
                            matchedSkill = characterToUpdate.Skills.FirstOrDefault(x => x.Skill == skill);
                        }
                        else
                        {
                            characterToUpdate.Skills = new List<DbCharacterSkills>();
                        }

                        if (matchedSkill == null)
                        {
                            characterToUpdate.Skills.Add(new DbCharacterSkills
                            {
                                Skill = skill,
                                SkillId = skill.SkillId,
                                Character = characterToUpdate,
                                CharacterId = characterToUpdate.Id
                            });
                        }
                        else
                        {
                            matchedSkill.Skill = skill;
                        }
                    }

                    charactersToUpdate.Add(characterToUpdate);
                    
                }

            }
            dbContext.UpdateRange(charactersToUpdate);
            dbContext.SaveChanges();
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

        public DbEquipment GetEquipmentByName(string name)
        {
            return dbContext.Equipment.FirstOrDefault(x => x.Equipment == name);
        }

        public DbSkills GetSkillByNameAndValue(string name, int value)
        {
            return dbContext.Skills.FirstOrDefault(x => x.SkillName == name && x.SkillValue == value);
        }

        public IList<DbSkills> UpdateOrAddSkillsFromSheet(Dictionary<string, int> skills)
        {
            var skillsToAddOrUpdate = new List<DbSkills>();
            foreach (var skill in skills)
            {
                var matchedSkill = GetSkillByNameAndValue(skill.Key, skill.Value);
                if (matchedSkill == null)
                {
                    skillsToAddOrUpdate.Add(new DbSkills
                    {
                        SkillName = skill.Key,
                        SkillValue = skill.Value
                    });
                }
                else
                {
                    skillsToAddOrUpdate.Add(matchedSkill);
                }
            }

            dbContext.UpdateRange(skillsToAddOrUpdate);
            dbContext.SaveChanges();
            return skillsToAddOrUpdate;
        }

        public IList<DbEquipment> UpdateOrAddEquipmentFromSheet(List<string> equipment)
        {
            var equipmentToAddOrUpdate = new List<DbEquipment>();
            foreach (var equip in equipment)
            {
                var matchedEquip = GetEquipmentByName(equip);
                if (matchedEquip == null)
                {
                    equipmentToAddOrUpdate.Add(new DbEquipment
                    {
                        Equipment = equip
                    });
                }
                else
                {
                    equipmentToAddOrUpdate.Add(matchedEquip);
                }
            }

            dbContext.UpdateRange(equipmentToAddOrUpdate);
            dbContext.SaveChanges();
            return equipmentToAddOrUpdate;
        }

        public DbCharacters GetCharacterByName(string name)
        {
            return dbContext.Characters.
                Include(x => x.Soulbreaks).
                Include(x => x.Equipment).
                ThenInclude(x => x.Equipment).
                Include(x => x.Skills).
                ThenInclude(x => x.Skill).
                FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
