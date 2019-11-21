using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbMagicitePassives
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PassiveId { get; set; }
        public string PassiveName { get; set; }
        public int PassiveValue { get; set; }

        [ForeignKey("Magicite")]
        public int MagiciteId { get; set; }
        public virtual DbMagicite Magicite { get; set; }
        
    }
}
