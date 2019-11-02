using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbEquipment
    {
        [Key]
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public virtual ICollection<DbCharacterEquipment> Characters { get; set; }

    }
}
