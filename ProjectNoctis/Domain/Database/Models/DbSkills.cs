using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbSkills
    {
        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int SkillValue { get; set; }
        public virtual ICollection<DbCharacterSkills> Characters {get; set;}
    }
}
