using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetUniqueEquipmentSets
    {
        public string Character { get; set; }
        public string Realm { get; set; }
        public string Season { get; set; }
        public string Group { get; set; }
        public string TwoSetBonus { get; set; }
        public string ThreeSetBonus { get; set; }
    }
}
