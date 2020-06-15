using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class CharacterDive
    {
        public SheetRecordBoards Board { get; set; }
        public SheetLegendSpheres LegendDive { get; set; }
        public SheetRecordSpheres RecordDive { get; set; }
    }
}
