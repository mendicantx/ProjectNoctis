using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetCrystalForceAbilities
    {
        public string Character { get; set; }
        public string Source { get; set; }
        public string SoulbreakVersion { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Formula { get; set; }
        public string Multiplier { get; set; }
        public string Element { get; set; }
        public string Time { get; set; }
        public string Effects { get; set; }
        public string AutoTarget { get; set; }
        public string SB { get; set; }
        public string School { get; set; }
        public string JPName { get; set; }
        public string ID { get; set; }
        public string AbilityImage
        {
            get
            {
                return $"https://dff.sp.mbga.jp/dff/static/lang/image/crystal_force/crystal_force.png";
            }
        }

    }
}
