using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Status
    {
        public SheetStatus Info { get; set; }
        public Dictionary<string, List<SheetStatus>> Statuses{ get; set; }
        public Dictionary<string, List<SheetOthers>> StatusOthers { get; set; }
    }
}
