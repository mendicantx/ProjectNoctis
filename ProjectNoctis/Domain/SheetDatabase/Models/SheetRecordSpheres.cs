using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetRecordSpheres
    {
        public string Character { get; set; }
        public Dictionary<string,int> Stats { get; set; }
        public List<string> Misc { get; set; }
        
    }
}
