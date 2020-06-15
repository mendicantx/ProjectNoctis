using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IDiveRepository
    {
        SheetRecordBoards GetRecordBoardByCharacterName(string name);

        SheetRecordSpheres GetRecordDiveByCharacterName(string name);

        SheetLegendSpheres GetLegendDiveByCharacterName(string name);
    }
}
