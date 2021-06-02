using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class UniqueEquipment
    {
        public string CharacterUrl { get; set; }
        public List<UniqueEquip> UniqueEquipments { get; set; }
        public UniqueEquipmentSet UniqueEquipmentSets { get; set; }
    }
}
