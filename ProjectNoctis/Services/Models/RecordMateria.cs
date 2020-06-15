using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class RecordMateria
    {
        public SheetRecordMaterias Info { get; set; }
        public Dictionary<string, List<SheetStatus>> Statuses { get; set; }
        public Dictionary<string, List<SheetOthers>> Others { get; set; }

    }
}
