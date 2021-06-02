using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class UniqueEquipmentSet
    {
        public SheetUniqueEquipmentSets Info { get; set; }
        public Dictionary<string, List<SheetStatus>> Statuses { get; set; }
    }
}
