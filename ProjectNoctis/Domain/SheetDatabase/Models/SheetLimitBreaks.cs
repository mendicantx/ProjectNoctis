using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetLimitBreaks
    {
        public string Name { get; set; }
        public string Realm { get; set; }
        public string Character { get; set; }
        public string Tier { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string Element { get; set; }
        public string Effects { get; set; }
        public string Minimum { get; set; }
        public string Multiplier { get; set; }
        public string Formula { get; set; }
        public string Target { get; set; }
        public string Relic { get; set; }
        public string JPName { get; set; }
        public string ID { get; set; }
        public string LimitBonus { get; set; }
        public string LimitBreakImage
        {
            get
            {
                return $"{Constants.Constants.ffrkImageBaseUrl}soulstrike/{ID}/{ID}_256.png";
            }
        }
    }
}
