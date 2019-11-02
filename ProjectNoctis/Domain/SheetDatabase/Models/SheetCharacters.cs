using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetCharacters
    {
        public string CharacterId { get; set; }
        public string Realm { get; set; }
        public string Name { get; set; }
        public int BaseHp { get; set; }
        public int BaseAtk { get; set; }
        public int BaseDef { get; set; }
        public int BaseMag { get; set; }
        public int BaseRes { get; set; }
        public int BaseMnd { get; set; }
        public int BaseAcc { get; set; }
        public int BaseEva { get; set; }
        public int BaseSpd { get; set; }
        public List<string> Equipment { get; set; }
        public Dictionary<string, int> Skills {get; set;}



    }
}
