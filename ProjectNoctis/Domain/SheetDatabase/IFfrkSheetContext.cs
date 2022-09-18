using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase
{
    public interface IFfrkSheetContext
    {
        List<SheetAbilities> Abilities { get; set; }
        List<SheetZenithAbilities> ZenithAbilities { get; set; }
        List<SheetBraves> Braves { get; set; }
        List<SheetBursts> Bursts { get; set; }
        List<SheetCharacters> Characters { get; set; }
        List<SheetLegendMaterias> LegendMaterias { get; set; }
        List<SheetLegendSpheres> LegendSpheres { get; set; }
        List<SheetMagicites> Magicites { get; set; }
        List<SheetRecordBoards> RecordBoards { get; set; }
        List<SheetRecordMaterias> RecordMaterias { get; set; }
        List<SheetRecordSpheres> RecordSpheres { get; set; }
        List<SheetSoulbreaks> Soulbreaks { get; set; }
        List<SheetLimitBreaks> LimitBreaks { get; set; }
        List<SheetStatus> Statuses { get; set; }
        List<SheetOthers> Others { get; set; }
        List<SheetSynchros> Synchros { get; set; }
        bool LastUpdateSuccessful { get; set; }
        DateTime LastUpdateTime { get; set; }
        List<SheetGuardianSummons> GuardianSummons { get; set; }
        List<SheetUniqueEquipment> UniqueEquipment { get; set; }
        List<SheetUniqueEquipmentSets> UniqueEquipmentSets { get; set; }

        Task<bool> SetupProperties();
    }
}