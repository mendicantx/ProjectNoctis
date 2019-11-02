using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetRecordBoards
    {
        public string Character { get; set; }
        public Dictionary<string,int> BoardBonusStats { get; set; }
        public List<String> BoardMisc { get; set; }
        public Dictionary<string,int> MotesRequired { get; set; }
    }
}
