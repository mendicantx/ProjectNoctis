using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface IDiveService
    {
        CharacterDive BuildRecordBoardByCharacter(string name);

        CharacterDive BuildRecordDiveByCharacter(string name);

        CharacterDive BuildLegendDiveByCharacter(string name);

        CharacterDive BuildFullDiveByCharacter(string name);
    }
}
