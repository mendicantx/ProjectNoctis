using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class SheetUpdateService : ISheetUpdateService
    {
        private readonly ICharacterRepository characterRepository;
        private readonly ISoulbreakRepository soulbreakRepository;
        private readonly IMagiciteRepository magiciteRepository;
        private readonly IStatusRepository statusRepository;

        //TODO: Make a service layer
        public SheetUpdateService(ICharacterRepository characterRepository, ISoulbreakRepository soulbreakRepository,
            IMagiciteRepository magiciteRepository, IStatusRepository statusRepository)
        {
            this.characterRepository = characterRepository;
            this.soulbreakRepository = soulbreakRepository;
            this.magiciteRepository = magiciteRepository;
            this.statusRepository = statusRepository;
        }

        public bool UpdateDatabase()
        {
            try
            {

                FfrkSheetContext sheetContext = null;
                try
                {
                    sheetContext = new FfrkSheetContext();
                }
                catch(Exception ex)
                {

                }

                
                magiciteRepository.AddOrUpdateMagicitesFromSheet(sheetContext.Magicites);
                characterRepository.UpdateCharactersFromSheet(sheetContext.Characters);
                characterRepository.UpdateLegendSpheresFromSheet(sheetContext.LegendSpheres);
                characterRepository.UpdateRecordSpheresFromSheet(sheetContext.RecordSpheres);
                characterRepository.UpdateRecordBoardsFromSheet(sheetContext.RecordBoards);

                var charNames = characterRepository.GetAllCharacterNames();
                soulbreakRepository.UpdateSoulbreaksFromSheet(sheetContext.Soulbreaks, charNames);
                soulbreakRepository.UpdateRecordMateriaFromSheet(sheetContext.RecordMaterias);
                soulbreakRepository.UpdateLegendMateriaFromSheet(sheetContext.LegendMaterias);
                soulbreakRepository.UpdateBraveCommandsFromSheet(sheetContext.Braves);
                soulbreakRepository.UpdateBurstCommandsFromSheet(sheetContext.Bursts);
                soulbreakRepository.UpdateSynchroCommandsFromSheet(sheetContext.Synchros);
                statusRepository.UpdateOrAddStatusesFromSheet(sheetContext.Statuses);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
