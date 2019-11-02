using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbCharacterSkills
    {
        public int SkillId { get; set; }
        public virtual DbSkills Skill { get; set; }
        public int CharacterId { get; set; }
        public virtual DbCharacters Character {get; set;}
    }
}
