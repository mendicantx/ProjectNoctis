using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Magicite
    {
        public SheetMagicites Info { get; set; }

        public Dictionary<string, List<SheetStatus>> MagiciteStatuses { get; set; }
    }
}
