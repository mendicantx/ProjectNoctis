using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetMagicites
    {
        public string Name { get; set; }
        public string Element { get; set; }
        public int Rarity { get; set; }
        public string Realm { get; set; }
        public Dictionary<string,int> Passives { get; set; }
        public string MagiciteUltra { get; set; }
        public string Type { get; set; }
        public string Formula { get; set; }
        public string Multiplier { get; set; }
        public string UltraElement { get; set; }
        public string Time { get; set; }
        public string Effects { get; set; }
        public string MagiciteId { get; set; }
        public string JPName { get; set; }
    }
}
