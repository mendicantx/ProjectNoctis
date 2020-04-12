using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbCharacters
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string CharacterId { get; set; }
        public string Realm { get; set; }
        public string Name { get; set; }
        public int BaseHp { get; set; }
        public int BaseAtk { get; set; }
        public int BaseDef { get; set; }
        public int BaseMag { get; set; }
        public int BaseRes { get; set; }
        public int BaseMnd { get; set; }
        public int BaseAcc { get; set; }
        public int BaseEva { get; set; }
        public int BaseSpd { get; set; }
        public virtual ICollection<DbCharacterSkills> CharacterSkills { get; set; }
        public virtual ICollection<DbCharacterEquipment> CharacterEquipment { get; set; }

        public virtual ICollection<DbSoulbreaks> Soulbreaks { get; set; }
        public virtual ICollection<DbLegendMaterias> LegendMaterias { get; set; }
        public virtual ICollection<DbRecordMaterias> RecordMaterias { get; set; }
        public virtual DbLegendSpheres LegendSphere { get; set; }
        public virtual DbRecordSpheres RecordSphere { get; set; }
        public virtual DbRecordBoards RecordBoard { get; set; }


        [NotMapped]
        public ICollection<DbSkills> Skills
        {
            get
            {
                if (CharacterSkills != null)
                {
                    return CharacterSkills.Select(x => x.Skill).ToList();
                }
                return null;
            }
        }

        [NotMapped]
        public ICollection<DbEquipment> Equipment
        {
            get
            {
                if (CharacterEquipment != null)
                {
                    return CharacterEquipment.Select(x => x.Equipment).ToList();
                }
                return null;
            }
        }
    }
}
