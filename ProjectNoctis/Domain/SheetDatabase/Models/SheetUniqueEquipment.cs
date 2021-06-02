using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetUniqueEquipment
    {
        public string Character { get; set; }
        public string Realm { get; set; }
        public string Season { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Synergy { get; set; }
        public string Combine { get; set; }
        public string Rarity { get; set; }
        public string Level { get; set; }
        public string Atk { get; set; }
        public string Def { get; set; }
        public string Mag { get; set; }
        public string Res { get; set; }
        public string Mnd { get; set; }
        public string Acc { get; set; }
        public string Eva { get; set; }
        public string FixedPassives { get; set; }
        public string RandomPassives { get; set; }
        public string Id { get; set; }
        public string EquipmentImage
        {
            get
            {
                return $"{Constants.Constants.ffrkImageABBaseUrl}equipment/{Id}/{Id}_101_256.png";
            }
        }
    }
}
