using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class DiveService : IDiveService
    {
        private readonly IDiveRepository diveRepository;

        public DiveService(IDiveRepository diveRepository)
        {
            this.diveRepository = diveRepository;
        }

        public CharacterDive BuildLegendDiveByCharacter(string name)
        {
            var dive = new CharacterDive();

            var legendDive = diveRepository.GetLegendDiveByCharacterName(name);
            dive.LegendDive = legendDive;

            return dive;
        }

        public CharacterDive BuildRecordDiveByCharacter(string name)
        {
            var dive = new CharacterDive();

            var recordDive = diveRepository.GetRecordDiveByCharacterName(name);
            dive.RecordDive = recordDive;

            return dive;
        }

        public CharacterDive BuildRecordBoardByCharacter(string name)
        {
            var dive = new CharacterDive();

            var recordBoard = diveRepository.GetRecordBoardByCharacterName(name);
            dive.Board = recordBoard;

            return dive;
        }

        public CharacterDive BuildFullDiveByCharacter(string name)
        {
            var dive = new CharacterDive();

            var recordBoard = diveRepository.GetRecordBoardByCharacterName(name);
            var recordDive = diveRepository.GetRecordDiveByCharacterName(name);
            var legendDive = diveRepository.GetLegendDiveByCharacterName(name);

            dive.Board = recordBoard;
            dive.RecordDive = recordDive;
            dive.LegendDive = legendDive;

            return dive;
        }
    }
}
