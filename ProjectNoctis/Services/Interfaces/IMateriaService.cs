using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface IMateriaService
    {
        List<LegendMateria> BuildLegendMateriaInfoByName(string name);

        List<RecordMateria> BuildRecordMateriaInfoByName(string name);
    }
}
