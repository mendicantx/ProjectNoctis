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
        public virtual ICollection<DbCharacterSkills> Skills { get; set; }
        public virtual ICollection<DbCharacterEquipment> Equipment { get; set; }
        public virtual ICollection<DbSoulbreaks> Soulbreaks { get; set; }
    }
}
