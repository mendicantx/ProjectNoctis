using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbCharacterEquipment
    {
        public int EquipmentId { get; set; }
        public virtual DbEquipment Equipment { get; set; }
        public int CharacterId { get; set; }
        public virtual DbCharacters Character { get; set;}
    }
}

