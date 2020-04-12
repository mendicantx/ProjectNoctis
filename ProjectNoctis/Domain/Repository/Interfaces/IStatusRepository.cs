using System.Collections.Generic;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IStatusRepository
    {
        DbStatuses GetStatusByStatusId(string statusId);
        void UpdateOrAddStatusesFromSheet(IList<SheetStatus> statuses);
    }
}